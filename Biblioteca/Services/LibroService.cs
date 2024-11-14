using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.Services
{
    public class LibroService : Libro
    {
        public LibroService(string autor, string titulo, string isbn, bool disponible = true)
        {
            Autor = autor;
            Titulo = titulo;
            ISBN = isbn;
            Disponible = disponible;
        }

        public void CambiarDisponibilidad(bool estado)
        {
            if (estado == true)
            {
                Console.WriteLine("VUELVE A ESTAR DISPONIBLE");
            }
            else
            {
                Console.WriteLine("AHORA ES SUYO");
            }
            Disponible = estado;
        }
    }
}
