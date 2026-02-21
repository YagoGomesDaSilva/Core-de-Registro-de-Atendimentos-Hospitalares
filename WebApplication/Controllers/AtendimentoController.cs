using Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class AtendimentoController : Controller
    {
        // GET: Atendimento
        private readonly IAtendimentoService _atendimentoService;
        private readonly IPacienteService _pacienteService;

        public AtendimentoController(IAtendimentoService atendimentoService, IPacienteService pacienteService)
        {
            _atendimentoService = atendimentoService;
            _pacienteService = pacienteService;
        }

        public ActionResult Index()
        {
            var historico = _atendimentoService.ObterHistorico();
            return View(historico);
        }

        public ActionResult Create()
        {
            CarregarViewBagPacientes();
            return View(new Atendimento());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Atendimento atendimento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _atendimentoService.RegistrarAtendimento(atendimento);
                    return RedirectToAction("Index");
                }
                catch (DomainException ex)
                {
                    // Aqui a regra de barramento do status "Ativo" será exibida ao usuário
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao registrar o atendimento.");
                }
            }

            CarregarViewBagPacientes();
            return View(atendimento);
        }

        private void CarregarViewBagPacientes()
        {
            var pacientes = _pacienteService.ObterTodos().ToList();
            ViewBag.PacienteId = new SelectList(pacientes, "Id", "Nome");
        }
    }
}