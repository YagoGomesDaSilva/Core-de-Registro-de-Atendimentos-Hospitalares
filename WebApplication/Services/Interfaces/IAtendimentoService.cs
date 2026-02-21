using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Services.Interfaces
{
    public interface IAtendimentoService
    {
        void RegistrarAtendimento(Atendimento atendimento);
        IEnumerable<Atendimento> ObterHistorico();
    }
}
