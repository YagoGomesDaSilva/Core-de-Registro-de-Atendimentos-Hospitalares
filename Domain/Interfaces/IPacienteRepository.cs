using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Paciente Adicionar(Paciente paciente);
        Paciente ObterPorId(int id);
        IEnumerable<Paciente> ObterTodos();
        Paciente Atualizar(Paciente paciente);
        bool Remover(int id);
        IEnumerable<Paciente> ObterPacientesComAtendimentoAtivo();
    }
}
