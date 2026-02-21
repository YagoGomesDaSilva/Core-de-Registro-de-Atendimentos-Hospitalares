using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.DTO;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public void Adicionar(Paciente paciente)
        {
            // Limpa a formatação do CPF para garantir apenas números
            paciente.Cpf = paciente.Cpf?.Replace(".", "").Replace("-", "").Trim();

            if (!CpfEhValido(paciente.Cpf))
            {
                throw new DomainException("O CPF informado possui um formato ou dígito verificador inválido.");
            }

            // Regra extra implícita: não permitir CPF duplicado
            var cpfJaExiste = _pacienteRepository.ObterTodos().Any(p => p.Cpf == paciente.Cpf);
            if (cpfJaExiste)
            {
                throw new DomainException("Já existe um paciente cadastrado com este CPF.");
            }

            _pacienteRepository.Adicionar(paciente);
        }

        public IEnumerable<Paciente> ObterTodos()
        {
            return _pacienteRepository.ObterTodos();
        }

        public Paciente ObterPorId(int id)
        {
            return _pacienteRepository.ObterPorId(id);
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