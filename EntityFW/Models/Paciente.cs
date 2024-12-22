namespace EntityFW.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int ObraSocialId { get; set; }
        public ObraSocial ObraSocial { get; set; }
        public Turno? Turno { get; set; }
        public List<Historial> Historiales { get; } = new();
    }
}
