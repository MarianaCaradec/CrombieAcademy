namespace EntityFW.Models
{
    //Uno a muchos
    public class ObraSocial
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Coseguro {  get; set; }
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
    }
}
