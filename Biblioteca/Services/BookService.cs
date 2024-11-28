using BibliotecaAPIWeb.Data;
using BibliotecaAPIWeb.InterfacesServices;
using BibliotecaAPIWeb.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace BibliotecaAPIWeb.Services
{
    public class BookService : IBookService
    {

        private readonly ExcelRepository _excelRepository;

        public BookService() 
        {
            _excelRepository = new ExcelRepository();
        }

        public void AddBook(BookDto book)
        {
            if (book == null) 
            {
                throw new ArgumentException("The book list cannot be null or empty.", nameof(book));
            }

            try
            {
                _excelRepository.CreateBookData(book);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to add books to the Excel database.", ex);
            }
        }

        public List<BookDto> GetAll()
        {
            return _excelRepository.GetDataBooks();
        }

        public BookDto GetBookByISBN(int isbn)
        {
            return _excelRepository.GetBookByISBN(isbn);
        }

        public BookDto GetBookByTitle(string title)
        {
            return _excelRepository.GetBookByTitle(title);
        }

        public Book Update(Book book)
        {
            if (book == null)
            {
                return null;
            }

            _excelRepository.UpdateBookDataByISBN(book);

            return book;
        }

        public void Delete(int isbn)
        {
            _excelRepository.DeleteBook(isbn);
        }

        private User ConvertToUser(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                UserType = userDto.UserType,
                BooksLoaned = userDto.BooksLoaned.Select(bookDto => new BookDto
                {
                    ISBN = bookDto.ISBN,
                    Author = bookDto.Author,
                    Title = bookDto.Title,
                    Available = bookDto.Available
                }).ToList(),
                MaxBooksAllowed = userDto.MaxBooksAllowed,
                CanAskALoan = userDto.CanAskALoan,
                LoanDays = userDto.LoanDays
            };
        }

        private Book ConvertToBook(BookDto bookDto)
        {
            return new Book
            {
                ISBN = bookDto.ISBN,
                Author = bookDto.Author,
                Title = bookDto.Title,
                Available = bookDto.Available,
                LoanDate = bookDto.LoanDate,
                ReturnDate = bookDto.ReturnDate
            };
        }


        public BookDto LoanBook(BookDto bookDto, UserDto userDto)
        {
            if (bookDto == null || !bookDto.Available)
            {
                throw new ArgumentException("The book cannot be null or unavailable.", nameof(bookDto));
            }

            if (userDto.BooksLoaned.Count >= userDto.MaxBooksAllowed)
            {
                userDto.CanAskALoan = false;
                throw new Exception("You cannot ask for more loans: please return a book first.");
            }

            userDto.CanAskALoan = true;
            userDto.BooksLoaned.Add(bookDto);
            bookDto.Available = false;
            bookDto.ReturnDate = bookDto.LoanDate.AddDays(userDto.LoanDays);

            var book = ConvertToBook(bookDto);
            var user = ConvertToUser(userDto);
            _excelRepository.UpdateBookDataByISBN(book);
            _excelRepository.UpdateUserDataById(user);

            Console.WriteLine($"Book succesfully loaned: you have to return it before {bookDto.ReturnDate:D}");
            return bookDto;
        }

        public BookDto ReturnBook(BookDto bookDto, UserDto userDto)
        {
            if (!userDto.BooksLoaned.Contains(bookDto))
            {
                throw new ArgumentException("The book isn't in your loaned books list.", nameof(bookDto));
            }

            if (DateTime.Now >= bookDto.ReturnDate)
            {
                userDto.CanAskALoan = false;
                bookDto.LoanDate = DateTime.Now.AddDays(7);
                throw new Exception("Your time for returning the book has expired: you wont be allowed to ask for a loan for one week");
            } else
            {
                userDto.CanAskALoan = true;
                Console.WriteLine($"Book succesfully returned, you have {userDto.BooksLoaned.Count} books, you can ask for a loan for {userDto.MaxBooksAllowed - userDto.BooksLoaned.Count} more");
            }

            userDto.BooksLoaned.Remove(bookDto);
            bookDto.Available = true;

            var book = ConvertToBook(bookDto);
            var user = ConvertToUser(userDto);
            _excelRepository.UpdateBookDataByISBN(book);
            _excelRepository.UpdateUserDataById(user);

            return bookDto;
        }
    }
}
