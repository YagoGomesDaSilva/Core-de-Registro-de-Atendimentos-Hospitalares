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
    public class PacienteRepository : IPacienteRepository
    {
        private readonly HospitalContext _context;

        public PacienteRepository(HospitalContext context)
        {
            _context = context;
        }

        public void Adicionar(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
        }

        public Paciente ObterPorId(int id)
        {
            return _context.Pacientes.Find(id);
        }

        public IEnumerable<Paciente> ObterTodos()
        {
            return _context.Pacientes.ToList();
        }

        public void Atualizar(Paciente paciente)
        {
            _context.Entry(paciente).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
                _context.SaveChanges();
            }
        }
    }
}
