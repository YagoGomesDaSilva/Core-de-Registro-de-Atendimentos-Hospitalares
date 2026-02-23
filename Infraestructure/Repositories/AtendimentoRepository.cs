using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class AtendimentoRepository : IAtendimentoRepository
    {
        private readonly HospitalContext _context;

        public AtendimentoRepository(HospitalContext context)
        {
            _context = context;
        }

        public Atendimento Adicionar(Atendimento atendimento)
        {
            _context.Atendimentos.Add(atendimento);
            _context.SaveChanges();
            return atendimento;
        }

        public Atendimento ObterPorId(int id)
        {
            return _context.Atendimentos
                .Include(a => a.Paciente)
                .FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Atendimento> ObterHistorico()
        {
            return _context.Atendimentos
                .Include(a => a.Paciente)
                .ToList();
        }

        public IEnumerable<Atendimento> ObterAtendimentosPorPaciente(int pacienteId)
        {
            return _context.Atendimentos
                .Where(a => a.PacienteId == pacienteId)
                .ToList();
        }

        public Atendimento Atualizar(Atendimento atendimento)
        {
            _context.Entry(atendimento).State = EntityState.Modified;
            _context.SaveChanges();
            return atendimento;
        }

        public bool Remover(int id)
        {
            var atendimento = _context.Atendimentos.Find(id);
            if (atendimento == null)
                return false;

            _context.Atendimentos.Remove(atendimento);
            _context.SaveChanges();
            return true;
        }
    }
}