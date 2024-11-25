using BibliotecaAPIWeb.Data;
using BibliotecaAPIWeb.InterfacesServices;
using BibliotecaAPIWeb.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace BibliotecaAPIWeb.Services
{
    public class BookService : IBookService
    {

        private readonly ExcelRepository _excelRepository;

        public BookService(ExcelRepository excelRepository) 
        {
            _excelRepository = excelRepository;
        }

        public void AddBook(Book book)
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

        public List<Book> GetAll()
        {
            return _excelRepository.GetDataBooks();
        }

        public Book GetBookByISBN(int isbn)
        {
            return _excelRepository.GetBookByISBN(isbn);
        }

        public Book GetBookByTitle(string title)
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

        public bool Delete(int isbn)
        {
            var deletedBook = _excelRepository.GetBookByISBN(isbn);

            if (deletedBook == null)
            {
                return false;
            }

            _excelRepository.GetDataBooks().Remove(deletedBook);
            return true;
        }

        public bool LoanBook(Book book, User user)
        {
            if (book == null || !book.Available)
            {
                throw new ArgumentException("The book cannot be null or unavailable.", nameof(book));
            }

            if (user.BooksLoaned.Count >= user.MaxBooksAllowed)
            {
                user.CanAskALoan = false;
                throw new Exception("You cannot ask for more loans: please return a book first.");
            }

            user.CanAskALoan = true;
            user.BooksLoaned.Add(book);
            book.Available = false;
            book.ReturnDate = book.LoanDate.AddDays(user.LoanDays);
            Console.WriteLine($"Book succesfully loaned: you have to return it before {book.ReturnDate:D}");
            return true;
        }

        public bool ReturnBook(Book book, User user)
        {
            if (!user.BooksLoaned.Contains(book))
            {
                throw new ArgumentException("The book isn't in your loaned books list.", nameof(book));
            }

            user.BooksLoaned.Remove(book);
            book.Available = true;

            if (DateTime.Now >= book.ReturnDate)
            {
                user.CanAskALoan = false;
                book.LoanDate = DateTime.Now.AddDays(7);
                throw new Exception("Your time for returning the book has expired: you wont be allowed to ask for a loan for one week");
            }
            user.CanAskALoan = true;
            Console.WriteLine($"Book succesfully returned, you have {user.BooksLoaned.Count} books, you can ask for a loan for {user.MaxBooksAllowed - user.BooksLoaned.Count} more");
            return true;
        }
    }
}
