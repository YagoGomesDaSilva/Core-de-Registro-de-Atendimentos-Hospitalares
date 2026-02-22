using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Paciente, PacienteDTO>()
                .ReverseMap();
                //.ForMember(dest => dest.DataRegisto, opt => opt.Ignore());

            CreateMap<Atendimento, AtendimentoDTO>().ReverseMap();
        }
    }
}
