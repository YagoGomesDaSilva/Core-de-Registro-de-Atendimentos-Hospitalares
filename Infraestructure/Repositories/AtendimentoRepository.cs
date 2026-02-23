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

        public void Adicionar(Atendimento atendimento)
        {
            _context.Atendimentos.Add(atendimento);
            _context.SaveChanges();
        }

        public Atendimento ObterPorId(int id)
        {
            return _context.Atendimentos.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Atendimento> ObterHistorico()
        {
            return _context.Atendimentos.ToList();
        }

        public IEnumerable<Atendimento> ObterAtendimentosPorPaciente(int pacienteId)
        {
            return _context.Atendimentos
                .Where(a => a.PacienteId == pacienteId)
                .ToList();
        }

        public void Atualizar(Atendimento atendimento)
        {
            _context.Entry(atendimento).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            var atendimento = _context.Atendimentos.Find(id);
            if (atendimento != null)
            {
                _context.Atendimentos.Remove(atendimento);
                _context.SaveChanges();
            }
        }
    }
}