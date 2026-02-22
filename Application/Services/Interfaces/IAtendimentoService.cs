using Application.DTO;
using System.Collections.Generic;

namespace Application.Services.Interfaces
{
    public interface IAtendimentoService
    {
        AtendimentoDTO RegistrarAtendimento(AtendimentoDTO atendimento);
        IEnumerable<AtendimentoDTO> ObterHistorico();
    }
}
