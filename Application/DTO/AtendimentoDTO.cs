using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class AtendimentoDTO
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataEntrada { get; set; } = DateTime.Now;
        public string StatusAtendimento { get; set; }
        public string PressaoArterial { get; set; }
        public decimal? Temperatura { get; set; }
        public int? FrequenciaCardiaca { get; set; }
        public PacienteDTO Paciente { get; set; }
    }
}
