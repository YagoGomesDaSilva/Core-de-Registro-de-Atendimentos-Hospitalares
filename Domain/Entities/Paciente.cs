using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataRegisto { get; set; }

        public virtual ICollection<Atendimento> Atendimentos { get; set; }

        public Paciente()
        {
            Atendimentos = new List<Atendimento>();
            DataRegisto = DateTime.Now;
        }
    }
}
