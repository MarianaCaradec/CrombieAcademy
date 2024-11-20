using BibliotecaAPIWeb.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BibliotecaAPIWeb.Services
{
    public class UsuarioService
    {
        private readonly List<Usuario> _usuarios = new List<Usuario>();

        public Usuario Crear(Usuario usuario)
        {
            usuario.Id = _usuarios.Count > 0 ? _usuarios.Max(u => u.Id) + 1 : 1;
            _usuarios.Add(usuario);
            return usuario;
        }

        public List<Usuario> ObtenerTodos()
        {
            return _usuarios;
        }

        public Usuario ObtenerUsuario(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }

        public bool Actualizar(int id, Usuario usuarioActualizado)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if(usuario == null)
            {
                return false;
            }

            usuario.Nombre = usuarioActualizado.Nombre;
            usuario.Prestados = usuarioActualizado.Prestados;
            return true;
        }

        public bool Borrar(int id)
        {
            var usuarioBorrado = _usuarios.FirstOrDefault(u => u.Id == id);

            if(usuarioBorrado == null)
            {
                return false;
            } 

            _usuarios.Remove(usuarioBorrado);
            return true;
        }
    }
}
