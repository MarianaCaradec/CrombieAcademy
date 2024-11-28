using BibliotecaAPIWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPIWeb.InterfacesServices
{
    public interface IBookService
    {
        Book Update(Book updatedBook);
        void AddBook(BookDto libro);
        void Delete(int isbn);
        BookDto GetBookByISBN(int isbn);
        BookDto GetBookByTitle(string title);
        List<BookDto> GetAll();
        BookDto LoanBook(BookDto book, UserDto user);
        BookDto ReturnBook(BookDto book, UserDto user);
    }
}