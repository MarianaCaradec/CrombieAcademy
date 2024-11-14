using System.Text.Json;

namespace BibliotecaAPIWeb.Models
{
    public class Coleccion
    {
        public List<Libro> Libros { get; set; }

        public List<Usuario> Usuarios { get; set; }

    }
}

