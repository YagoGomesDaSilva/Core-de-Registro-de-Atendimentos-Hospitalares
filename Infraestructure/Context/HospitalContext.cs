using Domain.Entities;
using Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class HospitalContext : DbContext
    {
        public HospitalContext() : base("name=HospitalDbConnection")
        {
            // Desabilita o carregamento preguiçoso para evitar problemas de serialização na API
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PacienteMap());
            modelBuilder.Configurations.Add(new AtendimentoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
