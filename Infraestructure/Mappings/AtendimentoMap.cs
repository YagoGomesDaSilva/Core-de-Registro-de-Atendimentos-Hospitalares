using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Mappings
{
    public class AtendimentoMap : EntityTypeConfiguration<Atendimento>
    {
        public AtendimentoMap()
        {
            ToTable("Atendimento");
            HasKey(a => a.Id);

            Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(a => a.DataEntrada)
                .IsRequired();

            Property(a => a.StatusAtendimento)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.PressaoArterial)
                .IsOptional()
                .HasMaxLength(20);

            Property(a => a.Temperatura)
                .IsOptional()
                .HasPrecision(5, 2);

            Property(a => a.FrequenciaCardiaca)
                .IsOptional();

            // Relacionamento Paciente 1 → N Atendimento
            HasRequired(a => a.Paciente)
                .WithMany(p => p.Atendimentos)
                .HasForeignKey(a => a.PacienteId)
                .WillCascadeOnDelete(false);
        }
    }
}