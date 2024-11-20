using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.Services
{
    public class LibroService
    {
        public List<Libro> _libros = new List<Libro>();

        public LibroService() {}

        public Libro Crear(Libro libro)
        {
            libro.ISBN = _libros.Count > 0 ? _libros.Max(l => l.ISBN) + 1 : 1;
            _libros.Add(libro);
            return libro;
        }

        public List<Libro> ObtenerTodos()
        {
            return _libros;
        }

        public Libro ObtenerISBN(int isbn)
        {
            return _libros.FirstOrDefault(l => l.ISBN == isbn);
        }

        public Libro ObtenerTitulo(string titulo)
        {
            return _libros.FirstOrDefault(l => l.Titulo == titulo);
        }

        public bool Actualizar(Libro libroActualizado, int isbn)
        {
            var libro = _libros.FirstOrDefault(l => l.ISBN == isbn);
            if(libro == null)
            {
                return false;
            }

            libro.Autor = libroActualizado.Autor;
            libro.Titulo = libroActualizado.Titulo;
            libro.Disponible = libroActualizado.Disponible;
            return true;
        }

        public bool Eliminar(int isbn)
        {
            var libroEliminado = _libros.FirstOrDefault(l => l.ISBN == isbn);
            if (libroEliminado == null)
            {
                return false;
            }

            _libros.Remove(libroEliminado);
            return true;
        }

        public void PrestarLibro(Libro libro, Usuario usuario)
        {
            if(!libro.Disponible)
            {
                Console.WriteLine("EL LIBRO QUE DESEA RETIRAR NO SE ENCUENTRA DISPONIBLE");
            }

            if (usuario.Prestados.Count >= usuario.MaxLibrosPermitidos)
            {
                libro.PuedePedirPrestamo = false;
                Console.WriteLine("NO ES POSIBLE REALIZAR MÁS PRÉSTAMOS: DEVUELVA ALGÚN LIBRO ANTES");
            }

            
             _libros.Remove(libro);
             libro.Disponible = false;
             libro.FechaDevolucion = libro.FechaPrestamo.AddDays(usuario.DiasPrestamo);
             Console.WriteLine($"LIBRO ADQUIRIDO CON ÉXITO: DEBE SER DEVUELTO ANTES DEL {libro.FechaDevolucion:D}");
    
        }

        public void RecibirLibroDevuelto(Libro libro, Usuario usuario)
        {
            if (!usuario.Prestados.Contains(libro))
            {
                Console.WriteLine("EL LIBRO QUE DESEA DEVOLVER NO SE ENCUENTRA ENTRE SUS LIBROS PRESTADOS");
            }

            if (DateTime.Now <= libro.FechaDevolucion)
            {
                Console.WriteLine($"LIBRO DEVUELTO CON ÉXITO: TENÉS {usuario.Prestados.Count} LIBROS, PODÉS PEDIR {usuario.Prestados.Count - usuario.MaxLibrosPermitidos} LIBROS MÁS");
            }
            else
            {
                libro.FechaPrestamo = DateTime.Now.AddDays(7);
                Console.WriteLine("HA EXPIRADO EL PLAZO DE DEVOLUCIÓN DE TU LIBRO: NO PODRÁS RETIRAR LIBROS PRESTADOS POR UNA SEMANA");
            }

            _libros.Add(libro);
            libro.Disponible = true;
        }

    }
}
