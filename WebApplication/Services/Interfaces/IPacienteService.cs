using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Services.Interfaces
{
    public interface IPacienteService
    {
        void Adicionar(Paciente paciente);
        IEnumerable<Paciente> ObterTodos();
        Paciente ObterPorId(int id);
    }
}
