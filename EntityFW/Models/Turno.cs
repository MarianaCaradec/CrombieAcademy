using Microsoft.Identity.Client;

namespace EntityFW.Models
{
    //Uno a uno
    public class Turno
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int ObraSocialId { get; set; }
        public ObraSocial ObraSocial { get; set; }
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public int Horario { get; set; }

    }
}
