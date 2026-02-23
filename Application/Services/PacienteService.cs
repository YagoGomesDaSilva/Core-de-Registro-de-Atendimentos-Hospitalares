using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.DTO;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;

        public PacienteService(IPacienteRepository pacienteRepository, IMapper mapper)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
        }

        public PacienteDTO Adicionar(PacienteDTO paciente)
        {
            // Limpa a formatação do CPF para garantir apenas números
            paciente.Cpf = paciente.Cpf?.Replace(".", "").Replace("-", "").Trim();

            if (!CpfEhValido(paciente.Cpf))
                throw new DomainException("O CPF informado possui um formato ou dígito verificador inválido.");

            // Regra extra implícita: não permitir CPF duplicado
            var cpfJaExiste = _pacienteRepository.ObterTodos().Any(p => p.Cpf == paciente.Cpf);
            if (cpfJaExiste)
                throw new DomainException("Já existe um paciente cadastrado com este CPF.");

            var entity = _mapper.Map<Paciente>(paciente);
            _pacienteRepository.Adicionar(entity);

            return _mapper.Map<PacienteDTO>(entity); 
        }

        public IEnumerable<PacienteDTO> ObterTodos()
        {
            var pacientes = _pacienteRepository.ObterTodos();
            return _mapper.Map<IEnumerable<PacienteDTO>>(pacientes);
        }

        public PacienteDTO ObterPorId(int id)
        {
            var paciente = _pacienteRepository.ObterPorId(id);
            return _mapper.Map<PacienteDTO>(paciente);
        }

        public IEnumerable<PacienteDTO> ObterPacientesComAtendimentoAtivo()
        {
            var pacientes = _pacienteRepository.ObterPacientesComAtendimentoAtivo();
            return _mapper.Map<IEnumerable<PacienteDTO>>(pacientes);
        }

        public PacienteDTO Atualizar(PacienteDTO paciente)
        {
            var entidadeExistente = _pacienteRepository.ObterPorId(paciente.Id);
            if (entidadeExistente == null)
                throw new DomainException("Paciente não encontrado.");

            if (!string.IsNullOrWhiteSpace(paciente.Cpf))
            {
                paciente.Cpf = paciente.Cpf?.Replace(".", "").Replace("-", "").Trim();

                if (!CpfEhValido(paciente.Cpf))
                    throw new DomainException("O CPF informado possui um formato ou dígito verificador inválido.");

                var cpfJaExiste = _pacienteRepository.ObterTodos()
                    .Any(p => p.Cpf == paciente.Cpf && p.Id != paciente.Id);
                if (cpfJaExiste)
                    throw new DomainException("Já existe outro paciente cadastrado com este CPF.");

                entidadeExistente.Cpf = paciente.Cpf;
            }

            if (!string.IsNullOrWhiteSpace(paciente.Nome))
                entidadeExistente.Nome = paciente.Nome;

            if (paciente.DataNascimento.HasValue) 
                entidadeExistente.DataNascimento = paciente.DataNascimento.Value;

            _pacienteRepository.Atualizar(entidadeExistente);

            var resultado = _mapper.Map<PacienteDTO>(entidadeExistente);
            resultado.Atendimentos = null; 
            return resultado;
        }

        public bool Remover(int id)
        {
            var entidadeExistente = _pacienteRepository.ObterPorId(id);
            if (entidadeExistente == null)
                throw new DomainException("Paciente não encontrado.");

            if (entidadeExistente.Atendimentos.Any())
                throw new DomainException("Não é possível remover um paciente que possui atendimentos registrados.");

            return _pacienteRepository.Remover(id);
        }

        private bool CpfEhValido(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}