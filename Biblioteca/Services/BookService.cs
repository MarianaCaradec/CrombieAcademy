using BibliotecaAPIWeb.Data;
using BibliotecaAPIWeb.InterfacesServices;
using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.Services
{
    public class BookService : IBookService
    {
        private readonly DataRepository _dataRepository;

        public BookService(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void AddBook(Book newBook)
        {
            try
            {
                _dataRepository.CreateBook(newBook);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to add books to the Excel database.", ex);
            }
        }

        public IEnumerable<Book> GetAll()
        {
            return _dataRepository.GetBooks();
        }

        public Book GetBookByISBN(string isbn)
        {
            return _dataRepository.GetBookByISBN(isbn);
        }

        public BookDto Update(BookDto book)
        {
            if (book == null)
            {
                return null;
            }

            _dataRepository.UpdateBookByISBN(book);

            return book;
        }

        public void Delete(string isbn)
        {
            _dataRepository.DeleteBook(isbn);
        }

        private UserDto ConvertToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserType = user.UserType,
                MaxBooksAllowed = user.MaxBooksAllowed,
                Sales = user.Sales
                .Where(sale => sale.Book != null)
                .Select(sale => new Sales
                {
                    ISBNBook = sale.ISBNBook,
                    LoanDate = sale.LoanDate,
                    ReturnDate = sale.ReturnDate,
                    UserId = user.Id,
                    Book = new Book
                    {
                        ISBN = sale.Book.ISBN,
                        Author = sale.Book.Author,
                        Title = sale.Book.Title,
                        Available = sale.Book.Available,
                    }
                }).ToList(),
            };
        }

        private BookDto ConvertToBookDto(Book book)
        {
            return new BookDto
            {
                ISBN = book.ISBN,
                Author = book.Author,
                Title = book.Title,
                Available = book.Available,
            };
        }


        public object LoanBook(Book book, User user)

        {
            if (book == null || !book.Available)
            {
                throw new ArgumentException("The book cannot be null or unavailable.", nameof(book));
            }

            Sales sales = new Sales
            {
                ISBNBook = book.ISBN,
                UserId = user.Id,
                LoanDate = DateTime.Now,
                ReturnDate = user.UserType == "Profesor"
                ? DateTime.Now.AddDays(14)
                : DateTime.Now.AddDays(7)
            };

            _dataRepository.InsertSale(sales);
            book.Available = false;
            _dataRepository.UpdateBookByISBN(ConvertToBookDto(book));

            user.Sales.Add(sales);
            _dataRepository.UpdateUserById(ConvertToUserDto(user));

            Console.WriteLine($"Book succesfully loaned: you have to return it before {sales.ReturnDate:D}");
            return book;
        }

        public object ReturnBook(Book book, User user)
        {
            if (book == null || user == null)
            {
                throw new ArgumentNullException("Book or User is null.");
            }

            Sales existingSale = user.Sales.FirstOrDefault(s => s.ISBNBook == book.ISBN && s.UserId == user.Id);

            if (existingSale == null)
            {
                throw new ArgumentException("The book isn't in your loaned books list.", nameof(book));
            }

            if (DateTime.Now >= existingSale.ReturnDate)
            {
                existingSale.LoanDate = DateTime.Now.AddDays(7);
                throw new Exception("Your time for returning the book has expired: you wont be allowed to ask for a loan for one week");
            }
            else
            {
                Console.WriteLine($"Book succesfully returned, you have {user.Sales.Count} books, you can ask for a loan for {user.MaxBooksAllowed - user.Sales.Count} more");
            }

            _dataRepository.DeleteSale(existingSale);
            book.Available = true;
            _dataRepository.UpdateBookByISBN(ConvertToBookDto(book));


            user.Sales.Remove(existingSale);
            _dataRepository.UpdateUserById(ConvertToUserDto(user));

            return book;
        }
    }
}

