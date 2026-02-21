using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AtendimentoRepository : IAtendimentoRepository
    {
        private readonly HospitalContext _context;

        public AtendimentoRepository(HospitalContext context)
        {
            _context = context;
        }

        public void Adicionar(Atendimento atendimento)
        {
            _context.Atendimentos.Add(atendimento);
            _context.SaveChanges();
        }

        public IEnumerable<Atendimento> ObterHistoricoCompleto()
        {
            // O .Include gera um INNER JOIN no banco de dados para trazer os dados do Paciente
            return _context.Atendimentos.Include("Paciente").ToList();
        }

        public IEnumerable<Atendimento> ObterAtendimentosPorPaciente(int pacienteId)
        {
            return _context.Atendimentos
                           .Where(a => a.PacienteId == pacienteId)
                           .ToList();
        }
    }
}