using Application.Services.Interfaces;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApplication.Api
{
    /// <summary>
    /// API para gerenciamento de pacientes
    /// </summary>
    public class PacienteController : ApiController
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        /// <summary>
        /// Obtém todos os pacientes cadastrados
        /// </summary>
        /// <returns>Lista de pacientes</returns>
        public IHttpActionResult Get()
        {
            var pacientes = _pacienteService.ObterTodos();
            return Ok(pacientes);
        }

        /// <summary>
        /// Obtém um paciente pelo ID
        /// </summary>
        /// <param name="id">ID do paciente</param>
        /// <returns>Dados do paciente</returns>
        public IHttpActionResult Get(int id)
        {
            var paciente = _pacienteService.ObterPorId(id);
            if (paciente == null)
                return NotFound();

            return Ok(paciente);
        }

        /// <summary>
        /// Cadastra um novo paciente
        /// </summary>
        /// <param name="paciente">Dados do paciente</param>
        /// <returns>Paciente cadastrado</returns>
        public IHttpActionResult Post([FromBody] PacienteDTO paciente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = _pacienteService.Adicionar(paciente);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um paciente existente
        /// </summary>
        public IHttpActionResult Put(int id, [FromBody] PacienteDTO paciente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                paciente.Id = id; // garante que o ID da URL prevalece
                var resultado = _pacienteService.Atualizar(paciente);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
