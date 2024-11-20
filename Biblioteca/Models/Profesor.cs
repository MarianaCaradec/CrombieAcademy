namespace BibliotecaAPIWeb.Models
{
    public class Profesor : Usuario
    {

        public override int MaxLibrosPermitidos => 5;

        public override int DiasPrestamo => 14;

        public Profesor(int id, string nombre, List<Libro> prestados) : base(id, nombre, prestados)
        {
            Id = id;
            Nombre = nombre;
            Prestados = prestados;
        }
    }
}
