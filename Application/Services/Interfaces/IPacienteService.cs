using Application.DTO;
using System.Collections.Generic;

namespace Application.Services.Interfaces
{
    public interface IPacienteService
    {
        PacienteDTO Adicionar(PacienteDTO paciente);
        IEnumerable<PacienteDTO> ObterTodos();
        PacienteDTO ObterPorId(int id);
    }
}
