using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class PacienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataRegisto { get; set; } = DateTime.Now;
        public ICollection<AtendimentoDTO> Atendimentos { get; set; }
    }
}
