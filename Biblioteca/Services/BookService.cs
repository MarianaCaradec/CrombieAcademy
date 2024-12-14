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

        public Book GetBookByTitle(string title)
        {
            return _dataRepository.GetBookByTitle(title);
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
            //CanAskALoan = user.CanAskALoan,
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

        //user.CanAskALoan = true;

        Sales sales = new Sales
        {
            ISBNBook = book.ISBN,
            UserId = user.Id,
            LoanDate = DateTime.Now,
        };

        user.Sales.Add(sales);
        book.Available = false;

        if (user.UserType == "Profesor")
        {
            sales.ReturnDate = sales.LoanDate.AddDays(14);
        }
        else
        {
            sales.ReturnDate = sales.LoanDate.AddDays(7);
        }

        var bookDto = ConvertToBookDto(book);
        var userDto = ConvertToUserDto(user);
        _dataRepository.UpdateBookByISBN(bookDto);
        _dataRepository.UpdateUserById(userDto);

        Console.WriteLine($"Book succesfully loaned: you have to return it before {sales.ReturnDate:D}");
        return book;
    }

    public object ReturnBook(Book book, User user)
    {
        Sales sales = new Sales
        {
           ISBNBook = book.ISBN,
           UserId = user.Id,
           LoanDate = DateTime.Now,
        };

            if (!user.Sales.Contains(sales))
            {
                throw new ArgumentException("The book isn't in your loaned books list.", nameof(book));
            }

            if (DateTime.Now >= sales.ReturnDate)
            {
                //user.CanAskALoan = false;
                sales.LoanDate = DateTime.Now.AddDays(7);
                throw new Exception("Your time for returning the book has expired: you wont be allowed to ask for a loan for one week");
            }
            else
            {
                //user.CanAskALoan = true;
                Console.WriteLine($"Book succesfully returned, you have {user.Sales.Count} books, you can ask for a loan for {user.MaxBooksAllowed - user.Sales.Count} more");
            }

        user.Sales.Remove(sales);
        book.Available = true;

        var bookDto = ConvertToBookDto(book);
        var userDto = ConvertToUserDto(user);
        _dataRepository.UpdateBookByISBN(bookDto);
        _dataRepository.UpdateUserById(userDto);

        return book;
    }
        }
    }

