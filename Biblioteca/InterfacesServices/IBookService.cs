using BibliotecaAPIWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPIWeb.InterfacesServices
{
    public interface IBookService
    {
        BookDto Update(BookDto updatedBook);
        void AddBook(Book newBook);
        void Delete(string isbn);
        Book GetBookByISBN(string isbn);
        IEnumerable<Book> GetAll();
        object LoanBook(Book book, User user);
        object ReturnBook(Book book, User user);
    }
}