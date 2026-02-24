using System;
using System.Collections.Generic;

namespace Application.DTO
{
    public class PacienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime DataRegisto { get; set; }
        public ICollection<AtendimentoDTO> Atendimentos { get; set; }
    }
}
