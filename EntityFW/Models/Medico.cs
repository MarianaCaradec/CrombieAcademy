namespace EntityFW.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string Disponibilidad { get; set; }
        public int Telefono { get; set; }
        public Turno? Turno { get; set; }
        public List<Historial> Historiales { get; } = new();

    }
}
