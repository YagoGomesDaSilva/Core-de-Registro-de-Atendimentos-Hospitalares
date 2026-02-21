using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Atendimento
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataEntrada { get; set; }
        public string StatusAtendimento { get; set; }
        public string PressaoArterial { get; set; }
        public decimal? Temperatura { get; set; }
        public int? FrequenciaCardiaca { get; set; }

        public virtual Paciente Paciente { get; set; }
    }
}
