using BibliotecaAPIWeb.Models;

namespace BibliotecaAPIWeb.Services
{
    public class EstudianteService 
    {
        private readonly List<Estudiante> _estudiantes = new List<Estudiante>();

        public Estudiante Crear(Estudiante estudiante)
        {
            estudiante.Id = _estudiantes.Count > 0 ? _estudiantes.Max(e => e.Id) + 1 : 1;
            _estudiantes.Add(estudiante);
            return estudiante;
        }

        public List<Estudiante> ObtenerTodos()
        {
            return _estudiantes;
        }

        public Estudiante ObtenerId(int id)
        {
            return _estudiantes.FirstOrDefault(e => e.Id == id);
        }

        public bool Actualizar(Estudiante estudianteActualizado, int id)
        {
            var estudiante = _estudiantes.FirstOrDefault(e => e.Id == id);
            if(estudiante == null)
            {
                return false;
            }

            estudiante.Id = estudianteActualizado.Id;
            estudiante.Nombre = estudianteActualizado.Nombre;
            estudiante.Prestados = estudianteActualizado.Prestados;
            return true;
        }

        public bool Eliminar(int id)
        {
            var estudianteEliminado = _estudiantes.FirstOrDefault(e => e.Id == id);
            if(estudianteEliminado == null)
            {
                return false;
            }

            _estudiantes.Remove(estudianteEliminado);
            return true;
        }
    }
}
