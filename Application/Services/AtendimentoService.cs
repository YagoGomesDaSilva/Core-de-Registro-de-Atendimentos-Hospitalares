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

        public AtendimentoDTO RegistrarAtendimento(AtendimentoDTO atendimento)
        {
            // Busca todos os atendimentos prévios deste paciente
            var atendimentosDoPaciente = _atendimentoRepository.ObterAtendimentosPorPaciente(atendimento.PacienteId);

            // Verifica a Regra de Negócio principal do desafio
            bool possuiAtendimentoAtivo = atendimentosDoPaciente.Any(a => a.StatusAtendimento.Equals("Ativo", StringComparison.OrdinalIgnoreCase));

            if (possuiAtendimentoAtivo)
                throw new DomainException("Impossível registrar: O paciente já possui um atendimento com status 'Ativo'. Finalize o atendimento atual antes de abrir um novo.");

            // Garante que o status inicial e a data estejam corretos
            atendimento.StatusAtendimento = "Ativo";
            atendimento.DataEntrada = DateTime.Now;

            var entity = _mapper.Map<Atendimento>(atendimento);
            _atendimentoRepository.Adicionar(entity);

            return _mapper.Map<AtendimentoDTO>(entity); 
        }

        public AtendimentoDTO ObterPorId(int id)
        {
            var atendimento = _atendimentoRepository.ObterPorId(id);
            return _mapper.Map<AtendimentoDTO>(atendimento);
        }

        public IEnumerable<AtendimentoDTO> ObterHistorico()
        {
            var atendimentos = _atendimentoRepository.ObterHistorico();
            return _mapper.Map<IEnumerable<AtendimentoDTO>>(atendimentos);
        }

        public AtendimentoDTO Atualizar(int id, AtendimentoDTO atendimento)
        {
            var entidadeExistente = _atendimentoRepository.ObterPorId(id);
            if (entidadeExistente == null)
                throw new DomainException("Atendimento não encontrado.");

            if (!string.IsNullOrWhiteSpace(atendimento.StatusAtendimento))
                entidadeExistente.StatusAtendimento = atendimento.StatusAtendimento;

            if (!string.IsNullOrWhiteSpace(atendimento.PressaoArterial))
                entidadeExistente.PressaoArterial = atendimento.PressaoArterial;

            if (atendimento.Temperatura.HasValue)
                entidadeExistente.Temperatura = atendimento.Temperatura.Value;

            if (atendimento.FrequenciaCardiaca.HasValue)
                entidadeExistente.FrequenciaCardiaca = atendimento.FrequenciaCardiaca.Value;

            _atendimentoRepository.Atualizar(entidadeExistente);

            return _mapper.Map<AtendimentoDTO>(entidadeExistente);
        }

        public bool Remover(int id)
        {
            var entidadeExistente = _atendimentoRepository.ObterPorId(id);
            if (entidadeExistente == null)
                throw new DomainException("Atendimento não encontrado.");

            if (entidadeExistente.StatusAtendimento.Equals("Ativo", StringComparison.OrdinalIgnoreCase))
                throw new DomainException("Não é possível remover um atendimento com status 'Ativo'. Finalize-o antes de excluir.");

            return _atendimentoRepository.Remover(id);
        }
    }
}