using Application.Services.Interfaces;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApplication.Api
{
    public class AtendimentoController : ApiController
    {
        private readonly IAtendimentoService _atendimentoService;
        private readonly IPacienteService _pacienteService;

        public AtendimentoController(IAtendimentoService atendimentoService, IPacienteService pacienteService)
        {
            _atendimentoService = atendimentoService;
            _pacienteService = pacienteService;
        }

        // GET api/atendimento
        public IHttpActionResult Get()
        {
            var historico = _atendimentoService.ObterHistorico();
            return Ok(historico);
        }

        // GET api/atendimento/pacientes
        [HttpGet]
        [Route("api/atendimento/pacientes")]
        public IHttpActionResult GetPacientes()
        {
            var pacientes = _pacienteService.ObterTodos();
            return Ok(pacientes);
        }

        // POST api/atendimento
        public IHttpActionResult Post([FromBody] AtendimentoDTO atendimento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _atendimentoService.RegistrarAtendimento(atendimento);
                return Ok(atendimento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}