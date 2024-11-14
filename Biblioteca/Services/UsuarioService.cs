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
}
