using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.Services
{
    public class Profesor : UsuarioService
    {
        public Profesor(string id, string nombre, List<Libro> prestados) : base(id, nombre, prestados)
        {
            FechaPrestamo = DateTime.Now;
            FechaDevolucion = FechaPrestamo.AddDays(14);
        }

        public void PedirPrestadoLibro(LibroService libro)
        {
            if (PuedePedirPrestamo && Prestados.Count <= 5)
            {
                base.PedirPrestadoLibro(libro);
                Console.WriteLine($"LIBRO ADQUIRIDO CON ÉXITO: TENÉS {Prestados.Count} LIBROS");
            }
            else
            {
                PuedePedirPrestamo = false;
                Console.WriteLine("NO ES POSIBLE REALIZAR MÁS PRÉSTAMOS: DEVUELVA ALGÚN LIBRO ANTES");
            }
        }

        public void DevolverLibroPrestado(LibroService libro)
        {
            base.DevolverLibroPrestado(libro);

            if (DateTime.Now <= FechaDevolucion)
            {
                PuedePedirPrestamo = true;
                Console.WriteLine($"LIBRO DEVUELTO CON ÉXITO: TENÉS {Prestados.Count} LIBROS");
            }
            else
            {
                PuedePedirPrestamo = false;
                FechaPrestamo = DateTime.Now.AddDays(7);
                Console.WriteLine("HA EXPIRADO EL PLAZO DE DEVOLUCIÓN DE TU LIBRO: NO PODRÁS RETIRAR LIBROS PRESTADOS POR UNA SEMANA");
            }
        }
    }
}
}
