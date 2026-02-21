using Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        public ActionResult Index()
        {
            var pacientes = _pacienteService.ObterTodos();
            return View(pacientes);
        }

        public ActionResult Create()
        {
            return View(new Paciente());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _pacienteService.Adicionar(paciente);
                    return RedirectToAction("Index");
                }
                catch (DomainException ex)
                {
                    // Captura a exceção de negócio e envia para a View
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao salvar o paciente.");
                }
            }
            return View(paciente);
        }
    }
}