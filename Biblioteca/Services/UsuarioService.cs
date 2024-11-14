using BibliotecaAPIWeb.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BibliotecaAPIWeb.Services
{
    public class UsuarioService : Usuario
    {
        public UsuarioService(string id, string nombre, List<Libro> prestados)
        {
            Id = id;
            Nombre = nombre;
            Prestados = prestados;
        }

        public void PedirPrestadoLibro(LibroService libro)
        {
            Prestados.Add(libro);
            FechaPrestamo = DateTime.Now;
            libro.CambiarDisponibilidad(false);
        }

        public void DevolverLibroPrestado(LibroService libro)
        {
            Prestados.Remove(libro);
            libro.CambiarDisponibilidad(true);
        }
    }

    public class Estudiante : UsuarioService
    {
        public Estudiante(string id, string nombre, List<Libro> prestados) : base(id, nombre, prestados)
        {
            FechaPrestamo = DateTime.Now;
            FechaDevolucion = FechaPrestamo.AddDays(7);
        }

        public void PedirPrestadoLibro(LibroService libro)
        {
            if (PuedePedirPrestamo && Prestados.Count < 3)
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
