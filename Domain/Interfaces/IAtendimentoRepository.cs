using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IAtendimentoRepository
    {
        Atendimento Adicionar(Atendimento atendimento);
        Atendimento ObterPorId(int id);
        IEnumerable<Atendimento> ObterHistorico();
        IEnumerable<Atendimento> ObterAtendimentosPorPaciente(int pacienteId);
        Atendimento Atualizar(Atendimento atendimento);
        bool Remover(int id);
    }
}
