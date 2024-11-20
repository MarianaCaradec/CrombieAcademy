using System;

namespace BibliotecaAPIWeb.Models
{
    public abstract class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<Libro> Prestados { get; set; }

        public abstract int MaxLibrosPermitidos { get; }

        public abstract int DiasPrestamo { get; }

        public Usuario(int id, string nombre, List<Libro> prestados)
        {
            Id = id;
            Nombre = nombre;
            Prestados = prestados;
        }

    }
}
