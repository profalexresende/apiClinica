using Microsoft.EntityFrameworkCore;
using apiClinica.Model;

namespace apiClinica.Data

{
    // Data/ApplicationDbContext.cs
    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<PlanoSaude> PlanosSaude { get; set; }
        public DbSet<PacientePlano> PacientePlanos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PacientePlano>()
                .HasKey(pp => new { pp.PacienteId, pp.PlanoSaudeId });

            modelBuilder.Entity<PacientePlano>()
                .HasOne(pp => pp.Paciente)
                .WithMany()
                .HasForeignKey(pp => pp.PacienteId);

            modelBuilder.Entity<PacientePlano>()
                .HasOne(pp => pp.PlanoSaude)
                .WithMany()
                .HasForeignKey(pp => pp.PlanoSaudeId);
        }
    }

}
