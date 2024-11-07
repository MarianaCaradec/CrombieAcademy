using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClasesObjetos
{
    public class Libro
    {
        public string Autor { get; set; }

        public string Titulo { get; set; }

        public string ISBN { get; set; }

        public bool Disponible { get; set; }

        public Libro(string autor, string titulo, string isbn, bool disponible = true)
        {
            Autor = autor;
            Titulo = titulo;
            ISBN = isbn;
            Disponible = disponible;
        }

        public void CambiarDisponibilidad(bool estado)
        {
            if(estado == true)
            {
                Console.WriteLine("VUELVE A ESTAR DISPONIBLE");
            } else
            {
                Console.WriteLine("AHORA ES SUYO");
            }
            Disponible = estado;
        }
    }

    public class User
    {
        public string Id { get; set; }

        public string Nombre { get; set; }

        public List<Libro> Prestados { get; set; }

        public User(string id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            Prestados = new List<Libro>();
        }

        public void RecibirLibroPrestado(Libro libro)
        {
            Prestados.Add(libro);
            libro.CambiarDisponibilidad(false);
        }

        public void DevolverLibroPrestado(Libro libro)
        {
            Prestados.Remove(libro);
            libro.CambiarDisponibilidad(true);
        }
    }

    public class Biblioteca
    {
        public List<Libro> Libros { get; set; }

        public List<User> Usuarios { get; set; }

        public Biblioteca()
        {
            Libros = new List<Libro>();
            Usuarios = new List<User>();
        }

        public void GuardarDatos()
        {
            var librosJson = JsonSerializer.Serialize(Libros);
            var usuariosJson = JsonSerializer.Serialize(Usuarios);

            File.WriteAllText("libros.json", librosJson);
            File.WriteAllText("usuarios.json", usuariosJson);
        }

        public void CargarDatos()
        {
            if(File.Exists("libros.json"))
            {
                var librosJson = File.ReadAllText("libros.json");
                Libros = JsonSerializer.Deserialize<List<Libro>>(librosJson) ?? new List<Libro>();
            }

            if(File.Exists("usuarios.json"))
            {
                var usuariosJson = File.ReadAllText("usuarios.json");
                Usuarios = JsonSerializer.Deserialize<List<User>>(usuariosJson) ?? new List<User>();
            }
        }

        public void AgregarLibro(Libro nuevoLibro)
        {
            if(Libros.Any(libro => libro.ISBN == nuevoLibro.ISBN))
            {
                Console.WriteLine("ESTE LIBRO YA EXISTE EN LA BIBLIOTECA, NO ES POSIBLE VOLVER A INGRESARLO");
            }

            Libros.Add(nuevoLibro);
            GuardarDatos();
        }

        public void IngresarUsuario(User nuevoUsuario)
        {
            if(Usuarios.Any(usuario => usuario.Id == nuevoUsuario.Id))
            {
                Console.WriteLine("ESTE USUARIO YA EXISTE, UTILIZA OTRO NOMBRE O ID");
            }
            Usuarios.Add(nuevoUsuario);
            GuardarDatos();
        }

        public Libro BuscarLibro(string isbn)
        {
            return Libros.Find(libro => libro.ISBN == isbn);
        }

        public User BuscarUsuario(string id)
        {
            return Usuarios.Find(usuario => usuario.Id == id);
        }
    }
}
