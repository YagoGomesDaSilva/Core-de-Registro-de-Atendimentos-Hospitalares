using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Paciente, PacienteDTO>().ReverseMap();
            CreateMap<Atendimento, AtendimentoDTO>().ReverseMap();
        }
    }
}
