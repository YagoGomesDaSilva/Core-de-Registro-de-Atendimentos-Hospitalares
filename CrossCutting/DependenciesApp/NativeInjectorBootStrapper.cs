using Application.Mappings;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DependenciesApp
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(Container container)
        {
            // O DbContext deve ser Scoped (uma instância por requisição HTTP)
            container.Register<HospitalContext>(Lifestyle.Scoped);

            // AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            mapperConfig.AssertConfigurationIsValid();
            container.RegisterInstance<IMapper>(mapperConfig.CreateMapper());

            // Repositórios
            container.Register<IPacienteRepository, PacienteRepository>(Lifestyle.Scoped);
            container.Register<IAtendimentoRepository, AtendimentoRepository>(Lifestyle.Scoped);
            // Services
            container.Register<IPacienteService, PacienteService>(Lifestyle.Scoped);
            container.Register<IAtendimentoService, AtendimentoService>(Lifestyle.Scoped);
        }
    }
}
