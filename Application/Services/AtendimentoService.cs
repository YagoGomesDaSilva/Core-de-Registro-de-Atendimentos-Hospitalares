using Application.DTO;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Services
{
    public class AtendimentoService : IAtendimentoService
    {
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IMapper _mapper;

        public AtendimentoService(IAtendimentoRepository atendimentoRepository, IMapper mapper)
        {
            _atendimentoRepository = atendimentoRepository;
            _mapper = mapper;
        }

        public void RegistrarAtendimento(AtendimentoDTO atendimento)
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

            var entity = _mapper.Map<Atendimento>(atendimento);
            _atendimentoRepository.Adicionar(entity);
        }

        public IEnumerable<AtendimentoDTO> ObterHistorico()
        {
            var atendimentos = _atendimentoRepository.ObterHistoricoCompleto();
            return _mapper.Map<IEnumerable<AtendimentoDTO>>(atendimentos);
        }
    }
}