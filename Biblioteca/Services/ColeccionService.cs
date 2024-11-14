using BibliotecaAPIWeb.Models;
using System.Text.Json;

namespace BibliotecaAPIWeb.Services
{
    public class ColeccionService : Coleccion
    {
        public ColeccionService()
        {
            Libros = new List<Libro>();
            Usuarios = new List<Usuario>();
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
            if (File.Exists("libros.json"))
            {
                var librosJson = File.ReadAllText("libros.json");
                Libros = JsonSerializer.Deserialize<List<Libro>>(librosJson) ?? new List<Libro>();
            }

            if (File.Exists("usuarios.json"))
            {
                var usuariosJson = File.ReadAllText("usuarios.json");
                Usuarios = JsonSerializer.Deserialize<List<Usuario>>(usuariosJson) ?? new List<Usuario>();
            }
        }

        public void AgregarLibro(Libro nuevoLibro)
        {
            if (Libros.Any(libro => libro.ISBN == nuevoLibro.ISBN))
            {
                Console.WriteLine("ESTE LIBRO YA EXISTE EN LA BIBLIOTECA, NO ES POSIBLE VOLVER A INGRESARLO");
            }

            Libros.Add(nuevoLibro);
            GuardarDatos();
        }

        public void IngresarUsuario(Usuario nuevoUsuario)
        {
            if (Usuarios.Any(usuario => usuario.Id == nuevoUsuario.Id))
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

        public Usuario BuscarUsuario(string id)
        {
            return Usuarios.Find(usuario => usuario.Id == id);
        }
    }
}
