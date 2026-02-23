using Application.DTO;
using System.Collections.Generic;

namespace Application.Services.Interfaces
{
    public interface IAtendimentoService
    {
        AtendimentoDTO RegistrarAtendimento(AtendimentoDTO atendimento);
        AtendimentoDTO Atualizar(int id, AtendimentoDTO atendimento);
        AtendimentoDTO ObterPorId(int id);
        IEnumerable<AtendimentoDTO> ObterHistorico();
        bool Remover(int id);
    }
}
