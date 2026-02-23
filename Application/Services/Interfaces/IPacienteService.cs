using Application.DTO;
using System.Collections.Generic;

namespace Application.Services.Interfaces
{
    public interface IPacienteService
    {
        PacienteDTO Adicionar(PacienteDTO paciente);
        PacienteDTO Atualizar(PacienteDTO paciente);
        PacienteDTO ObterPorId(int id);
        IEnumerable<PacienteDTO> ObterTodos();
        IEnumerable<PacienteDTO> ObterPacientesComAtendimentoAtivo();
        void Remover(int id);
    }
}
