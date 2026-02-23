using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IAtendimentoRepository
    {
        void Adicionar(Atendimento atendimento);
        Atendimento ObterPorId(int id);
        IEnumerable<Atendimento> ObterHistorico();
        IEnumerable<Atendimento> ObterAtendimentosPorPaciente(int pacienteId);
        void Atualizar(Atendimento atendimento);
        void Remover(int id);
    }
}
