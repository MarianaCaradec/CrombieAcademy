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


        // Acción que invoca el método Insert()
        [HttpPost("insert")]
        public IActionResult InsertBook(Book newBook)
        {
            try
            {
                _bookService.AddBook(newBook);
                return Ok("Libro insertado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar el libro: {ex.Message}");
            }
        }

        //GET: api/<LibroController>
        [HttpGet]
        public IActionResult GetAllLibros()
        {
            IEnumerable<Book> books = _bookService.GetAll();

            var book = books.Select(libro => new Book
            {
                Author = libro.Author,
                Title = libro.Title,
                ISBN = libro.ISBN,
                Available = libro.Available,
            }).ToList();

            return Ok(book);
        }

        // GET api/<LibroController>/5
        [HttpGet("{isbn}")]
        public Book Get(string isbn)
        {
            Book response = _bookService.GetBookByISBN(isbn);
            return response;
        }

        //POST api/<LibroController>
        [HttpPost]
        public Book Post([FromBody] Book newBook)
        {
            _bookService.AddBook(newBook);
            return newBook;

        }


        // PUT api/<LibroController>/5
        [HttpPut("{isbnActualizar}")]
        public IActionResult Update([FromBody] BookDto book)
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
        public void Delete(string isbn)
        {
            _bookService.Delete(isbn);
            return;
        }

        // POST api/<LibroController>/5
        [HttpPost("prestar/{ISBN}")]
        public IActionResult LoanBook(string ISBN, int userId)
        {
            Book loanedBook = _bookService.GetBookByISBN(ISBN);
            User user = _userService.GetUserById(userId);

            if (loanedBook == null || user == null)
            {
                return null;
            }

            var response = _bookService.LoanBook(loanedBook, user);
            return Ok(response);
        }

        // POST api/<LibroController>/5
        [HttpPost("devolver/{ISBN}")]
        public IActionResult ReturnBook(string ISBN, int userId)
        {
            Book returnedBook = _bookService.GetBookByISBN(ISBN);
            User user = _userService.GetUserById(userId);

            if (returnedBook == null || user == null)
            {
                return null;
            }

            var response = _bookService.ReturnBook(returnedBook, user);
            return Ok(response);
        }

    }
}
