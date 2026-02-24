using System;

namespace Domain.Entities
{
    public class Atendimento
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataEntrada { get; set; } = DateTime.Now;
        public string StatusAtendimento { get; set; } = "Ativo";
        public string PressaoArterial { get; set; }
        public decimal? Temperatura { get; set; }
        public int? FrequenciaCardiaca { get; set; }

        public virtual Paciente Paciente { get; set; }
    }
}
