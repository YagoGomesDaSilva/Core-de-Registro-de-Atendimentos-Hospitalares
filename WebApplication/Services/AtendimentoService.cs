using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class AtendimentoService : IAtendimentoService
    {
        private readonly IAtendimentoRepository _atendimentoRepository;

        public AtendimentoService(IAtendimentoRepository atendimentoRepository)
        {
            _atendimentoRepository = atendimentoRepository;
        }

        public void RegistrarAtendimento(Atendimento atendimento)
        {
            // Busca todos os atendimentos prévios deste paciente
            var atendimentosDoPaciente = _atendimentoRepository.ObterAtendimentosPorPaciente(atendimento.PacienteId);

            // Verifica a Regra de Negócio principal do desafio
            bool possuiAtendimentoAtivo = atendimentosDoPaciente.Any(a => a.StatusAtendimento.Equals("Ativo", StringComparison.OrdinalIgnoreCase));

            if (possuiAtendimentoAtivo)
            {
                throw new DomainException("Impossível registrar: O paciente já possui um atendimento com status 'Ativo'. Finalize o atendimento atual antes de abrir um novo.");
            }

            // Garante que o status inicial e a data estejam corretos
            atendimento.StatusAtendimento = "Ativo";
            atendimento.DataEntrada = DateTime.Now;

            _atendimentoRepository.Adicionar(atendimento);
        }

        public IEnumerable<Atendimento> ObterHistorico()
        {
            return _atendimentoRepository.ObterHistoricoCompleto();
        }
    }
}