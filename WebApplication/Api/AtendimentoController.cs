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
        /// Obtém um atendimento pelo ID
        /// </summary>
        /// <param name="id">ID do atendimento</param>
        /// <returns>Dados do atendimento</returns>
        public IHttpActionResult Get(int id)
        {
            var atendimento = _atendimentoService.ObterPorId(id);
            if (atendimento == null)
                return NotFound();

            return Ok(atendimento);
        }

        /// <summary>
        /// Obtém a lista de pacientes disponíveis para atendimento
        /// </summary>
        /// <returns>Lista de pacientes</returns>
        [HttpGet]
        [Route("api/atendimento/pacientesDisponiveis")]
        public IHttpActionResult GetPacientesDisponiveis()
        {
            var pacientes = _pacienteService.ObterPacientesComAtendimentoAtivo();
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
                var resultado = _atendimentoService.RegistrarAtendimento(atendimento);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um atendimento existente
        /// </summary>
        /// <param name="id">ID do atendimento</param>
        /// <param name="atendimento">Dados atualizados</param>
        public IHttpActionResult Put(int id, [FromBody] AtendimentoDTO atendimento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = _atendimentoService.Atualizar(id, atendimento);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove um atendimento pelo ID
        /// </summary>
        /// <param name="id">ID do atendimento</param>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _atendimentoService.Remover(id);
                return Ok(new { mensagem = "Atendimento removido com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}