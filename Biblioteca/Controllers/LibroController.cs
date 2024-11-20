using BibliotecaAPIWeb.Models;
using BibliotecaAPIWeb.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly LibroService _libroService;

        public LibroController(LibroService libroService)
        {
            _libroService = libroService;
        }

        // GET: api/<LibroController>
        [HttpGet]
        public List<Libro> GetAllLibros()
        {
            var respuesta = _libroService._libros.ToList();
            return respuesta;
        }

        // GET api/<LibroController>/5
        [HttpGet("{isbn}")]
        public Libro Get(int isbn)
        {
            var respuesta = _libroService._libros[isbn];
            return respuesta;
            
        }

        // POST api/<LibroController>
        [HttpPost]
        public Libro Post([FromBody] Libro nuevoLibro)
        {
            _libroService.Crear(nuevoLibro);
            return nuevoLibro;

        }

        // PUT api/<LibroController>/5
        [HttpPut("{titulo}")]
        public Libro PrestarLibro(string titulo, Usuario usuario)
        {
            var libroPrestado = _libroService.ObtenerTitulo(titulo);

            _libroService.PrestarLibro(libroPrestado, usuario);
            return libroPrestado;
        }

        // PUT api/<LibroController>/5
        [HttpPut("{titulo}")]
        public Libro DevolverLibro(string titulo, Usuario usuario)
        {
            var libroDevuelto = _libroService.ObtenerTitulo(titulo);

            _libroService.PrestarLibro(libroDevuelto, usuario);
            return libroDevuelto;
        }

        // PUT api/<LibroController>/5
        [HttpPut("{titulo, isbn}")]
        public Libro Update(int isbn, Usuario usuario)
        {
            var libroActualizado = _libroService.ObtenerISBN(isbn);

            _libroService.Actualizar(libroActualizado, isbn);
            return libroActualizado;
        }

        // DELETE api/<LibroController>/5
        [HttpDelete("{isbn}")]
        public void Delete(int isbn)
        {
            _libroService.Eliminar(isbn);
            return;
        }
    }
}
