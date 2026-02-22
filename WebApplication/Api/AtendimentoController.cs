using Application.Services.Interfaces;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApplication.Api
{
    /// <summary>
    /// API para gerenciamento de atendimentos hospitalares
    /// </summary>
    public class AtendimentoController : ApiController
    {
        private readonly IAtendimentoService _atendimentoService;
        private readonly IPacienteService _pacienteService;

        public AtendimentoController(IAtendimentoService atendimentoService, IPacienteService pacienteService)
        {
            _atendimentoService = atendimentoService;
            _pacienteService = pacienteService;
        }

        /// <summary>
        /// Obtém o histórico de atendimentos
        /// </summary>
        /// <returns>Lista de atendimentos realizados</returns>
        public IHttpActionResult Get()
        {
            var historico = _atendimentoService.ObterHistorico();
            return Ok(historico);
        }

        /// <summary>
        /// Obtém a lista de pacientes disponíveis para atendimento
        /// </summary>
        /// <returns>Lista de pacientes</returns>
        [HttpGet]
        [Route("api/atendimento/pacientes")]
        public IHttpActionResult GetPacientes()
        {
            var pacientes = _pacienteService.ObterTodos();
            return Ok(pacientes);
        }

        /// <summary>
        /// Registra um novo atendimento
        /// </summary>
        /// <param name="atendimento">Dados do atendimento</param>
        /// <returns>Atendimento registrado</returns>
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