using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.Services
{
    public class ProfesorService
    {
        private readonly List<Profesor> _profesores = new List<Profesor>();

        public Profesor Crear(Profesor profesor)
        {
            profesor.Id = _profesores.Count > 0 ? _profesores.Max(p => p.Id) + 1 : 1;
            _profesores.Add(profesor);
            return profesor;
        }

        public List<Profesor> ObtenerTodos()
        {
            return _profesores;
        }

        public Profesor ObetenerId(int id)
        {
            return _profesores.FirstOrDefault(p => p.Id == id);
        }

        public bool Actualizar(Profesor profesorActualizado, int id)
        {
            var profesor = _profesores.FirstOrDefault(p => p.Id == id);
            if (profesor == null)
            {
                return false;
            }

            profesor.Id = profesorActualizado.Id;
            profesor.Nombre = profesorActualizado.Nombre;
            profesor.Prestados = profesorActualizado.Prestados;
            return true;
        }

        public bool Eliminar(int id)
        {
            var profesorEliminado = _profesores.FirstOrDefault(p => p.Id == id);
            if (profesorEliminado == null)
            {
                return false;
            }

            _profesores.Remove(profesorEliminado);
            return true;
        }
    }
}
