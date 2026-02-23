using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly HospitalContext _context;

        public PacienteRepository(HospitalContext context)
        {
            _context = context;
        }

        public Paciente Adicionar(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
            return paciente;
        }

        public Paciente ObterPorId(int id)
        {
            return _context.Pacientes.Include("Atendimentos").FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Paciente> ObterTodos()
        {
            return _context.Pacientes.ToList();
        }

        public Paciente Atualizar(Paciente paciente)
        {
            _context.Entry(paciente).State = EntityState.Modified;
            _context.SaveChanges();
            return paciente;
        }

        public bool Remover(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null)
                return false;

            _context.Pacientes.Remove(paciente);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Paciente> ObterPacientesComAtendimentoAtivo()
        {
            return _context.Pacientes
                .Include("Atendimentos")
                .Where(p => p.Atendimentos.Any(a => a.StatusAtendimento == "Ativo"))
                .ToList();
        }
    }
}
