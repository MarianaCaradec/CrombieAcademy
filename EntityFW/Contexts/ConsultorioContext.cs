using EntityFW.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFW.Contexts
{
    public class ConsultorioContext : DbContext
    {
        DbSet<Paciente> Pacientes { get; set; }

        DbSet<Medico> Medicos { get; set; }

        DbSet<Turno> Turnos { get; set; }

        DbSet<ObraSocial> ObrasSociales { get; set; }

        DbSet<Historial> Historiales { get; set; }

        public ConsultorioContext(DbContextOptions<ConsultorioContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>(p =>
            {
                p.HasKey(p => p.Id);
                p.Property(p => p.DNI);
                p.Property(el => el.Nombre);
                p.Property(el => el.Email);
                p.HasOne(p => p.Turno)
                .WithOne(t => t.Paciente)
                .HasForeignKey<Turno>(t => t.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);
                p.HasMany(p => p.Historiales)
                .WithOne(h => h.Paciente)
                .HasForeignKey(h => h.PacienteId);
            });


            modelBuilder.Entity<ObraSocial>(os =>
            {
                os.HasKey(os => os.Id);
                os.Property(os => os.Nombre);
                os.Property(os => os.Coseguro);
                os.HasMany(p => p.Pacientes)
                .WithOne(p => p.ObraSocial)
                .HasForeignKey(p => p.ObraSocialId)
                .IsRequired();
            });

            modelBuilder.Entity<Medico>(m =>
            {
                m.HasKey(m => m.Id);
                m.Property(m => m.Nombre);
                m.Property(m => m.Especialidad);
                m.Property(m => m.Disponibilidad);
                m.Property(m => m.Telefono);
                m.HasOne(t => t.Turno)
                .WithOne(t => t.Medico)
                .HasForeignKey<Turno>(t => t.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);
                m.HasMany(m => m.Historiales)
                .WithOne(h => h.Medico)
                .HasForeignKey(h => h.MedicoId);
            });

            modelBuilder.Entity<Turno>(t =>
            {
                t.HasKey(t => t.Id);
                t.Property(t => t.Tipo);
                t.Property(t => t.Fecha);
                t.Property(t => t.Horario);
                t.HasOne(t => t.ObraSocial)
                .WithMany()
                .HasForeignKey(t => t.ObraSocialId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Historial>(h =>
            {
                h.HasKey(t => new {t.MedicoId, t.PacienteId});
                h.Property(h => h.Observacion);
            });
        }
    }
}
