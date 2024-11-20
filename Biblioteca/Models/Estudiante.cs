using DocumentFormat.OpenXml.Office2010.Excel;

namespace BibliotecaAPIWeb.Models
{
    public class Estudiante : Usuario
    {
        public override int MaxLibrosPermitidos => 3;

        public override int DiasPrestamo => 5; 

        public Estudiante(int id, string nombre, List<Libro> prestados) : base(id, nombre, prestados)
        {
            Id = id;
            Nombre = nombre;
            Prestados = prestados;
        }
    }
}
