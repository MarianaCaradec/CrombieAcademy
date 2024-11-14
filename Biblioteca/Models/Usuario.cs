using System;

namespace BibliotecaAPIWeb.Models
{
    public class Usuario
    {
        public string Id { get; set; }

        public string Nombre { get; set; }

        public List<Libro> Prestados { get; set; }

        public DateTime FechaPrestamo { get; set; }

        public DateTime FechaDevolucion { get; set; }

        public bool PuedePedirPrestamo { get; set; } = true;

    }
}
