using BibliotecaAPIWeb.Data;
using BibliotecaAPIWeb.InterfacesServices;
using BibliotecaAPIWeb.Models;
using BibliotecaAPIWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IUserService _userService;

        public BookController(IBookService bookService, IUserService userService)
        {
            _bookService = bookService;
            _userService = userService;
        }

        // GET: api/<LibroController>
        [HttpGet]
        public IActionResult GetAllLibros()
        {
            List<Book> books = _bookService.GetAll();

            var booksDto = books.Select(libro => new BookDto
            {
                Author = libro.Author,
                Title = libro.Title,
                ISBN = libro.ISBN,
                Available = libro.Available,
            }).ToList();

            return Ok(booksDto);
        }

        // GET api/<LibroController>/5
        [HttpGet("{isbn}")]
        public Book Get(int isbn)
        {
            Book response = _bookService.GetBookByISBN(isbn);
            return response;
        }

        // POST api/<LibroController>
        [HttpPost]
        public Book Post([FromBody] Book newBook)
        {
            _bookService.AddBook(newBook);
            return newBook;

        }

        // POST api/<LibroController>/5
        [HttpPost("prestar/{tituloPrestar}")]
        public IActionResult LoanBook(string title, int userId)
        {
            Book loanedBook = _bookService.GetBookByTitle(title);
            User user = _userService.GetUserById(userId);

            if(loanedBook == null || user == null)
            {
                return null;
            }

            var response = _bookService.LoanBook(loanedBook, user);
            return Ok(response);
        }

        // POST api/<LibroController>/5
        [HttpPost("devolver/{tituloDevolver}")]
        public IActionResult ReturnBook(string title, int userId)
        {
            Book returnedBook = _bookService.GetBookByTitle(title);
            User user = _userService.GetUserById(userId);

            if (returnedBook == null || user == null)
            {
                return null;
            }

            var response = _bookService.ReturnBook(returnedBook, user);
            return Ok(response);
        }

        // PUT api/<LibroController>/5
        [HttpPut("{isbnActualizar}")]
        public IActionResult Update([FromBody] Book book)
        {
            var updatedBook = _bookService.Update(book);

            if (book == null)
            {
                return null;
            }
            
            return Ok(updatedBook);
        }

        // DELETE api/<LibroController>/5
        [HttpDelete("{isbnEliminar}")]
        public void Delete(int isbn)
        {
            _bookService.Delete(isbn);
            return;
        }
    }
}
