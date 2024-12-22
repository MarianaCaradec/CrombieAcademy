namespace EntityFW.Models
{
    //Muchos a muchos
    public class Historial
    {
        public int MedicoId { get; set; }
        public Medico Medico { get; set; } = null;
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null;
        public string Observacion { get; set; }
    }
}
