namespace BibliotecaAPIWeb.Models
{
    public class Libro
    {
        public string Autor { get; set; }

        public string Titulo { get; set; }

        public int ISBN { get; set;  }

        public bool Disponible { get; set; }

        public DateTime FechaPrestamo { get; set; }

        public DateTime FechaDevolucion { get; set; }

        public bool PuedePedirPrestamo { get; set; } = true;

        public Libro(string autor, string titulo, int isbn, bool disponible, DateTime prestamo, DateTime devolucion)
        {
            Autor = autor;
            Titulo = titulo;
            ISBN = isbn;
            Disponible = disponible;
            FechaPrestamo = DateTime.Now;
            FechaDevolucion = devolucion;
        }

    }
}
