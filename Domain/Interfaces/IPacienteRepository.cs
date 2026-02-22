using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPacienteRepository
    {
        void Adicionar(Paciente paciente);
        Paciente ObterPorId(int id);
        IEnumerable<Paciente> ObterTodos();
        void Atualizar(Paciente paciente);
        void Remover(int id);
        IEnumerable<Paciente> ObterPacientesComAtendimentoAtivo();
    }
}
