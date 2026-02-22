using Application.Services.Interfaces;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication.Api
{
    public class PacienteController : ApiController
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        // GET api/paciente
        public IHttpActionResult Get()
        {
            var pacientes = _pacienteService.ObterTodos();
            return Ok(pacientes);
        }

        // GET api/paciente/5
        public IHttpActionResult Get(int id)
        {
            var paciente = _pacienteService.ObterPorId(id);
            if (paciente == null)
                return NotFound();

            return Ok(paciente);
        }

        // POST api/paciente
        public IHttpActionResult Post([FromBody] PacienteDTO paciente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _pacienteService.Adicionar(paciente);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
