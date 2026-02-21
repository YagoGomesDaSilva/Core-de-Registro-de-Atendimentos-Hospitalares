using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DependenciesApp
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // O DbContext deve ser Scoped (uma instância por requisição HTTP)
            services.AddScoped<HospitalContext>();

            // Repositórios
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();

            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IAtendimentoService, AtendimentoService>();
        }
    }
}
