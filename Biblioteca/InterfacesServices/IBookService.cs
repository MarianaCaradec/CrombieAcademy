using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.InterfacesServices
{
    public interface IBookService
    {
        Book Update(Book updatedBook);
        void AddBook(Book libro);
        bool Delete(int isbn);
        Book GetBookByISBN(int isbn);
        Book GetBookByTitle(string title);
        List<Book> GetAll();
        bool LoanBook(Book book, User user);
        bool ReturnBook(Book book, User user);
    }
}