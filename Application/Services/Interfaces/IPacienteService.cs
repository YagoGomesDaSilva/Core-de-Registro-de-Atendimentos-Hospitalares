using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IPacienteService
    {
        void Adicionar(PacienteDTO paciente);
        IEnumerable<PacienteDTO> ObterTodos();
        PacienteDTO ObterPorId(int id);
    }
}
