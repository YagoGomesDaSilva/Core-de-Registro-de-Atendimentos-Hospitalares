using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Mappings
{
    public class PacienteMap : EntityTypeConfiguration<Paciente>
    {
        public PacienteMap()
        {
            ToTable("Paciente");
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(p => p.Cpf)
                .IsRequired()
                .HasMaxLength(14)
                .IsFixedLength();

            Property(p => p.DataNascimento)
                .IsRequired();

            Property(p => p.DataRegisto)
                .IsRequired();
        }
    }
}