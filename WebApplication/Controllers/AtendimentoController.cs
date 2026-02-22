using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Application.DTO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class AtendimentoController : Controller
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private string GetApiBaseUrl()
        {
            return string.Format("{0}://{1}{2}api/",
                Request.Url.Scheme, Request.Url.Authority, Url.Content("~/"));
        }

        public ActionResult Index()
        {
            var apiUrl = GetApiBaseUrl() + "atendimento";
            var response = _httpClient.GetAsync(apiUrl).Result;
            var historico = new List<AtendimentoDTO>();

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                historico = JsonConvert.DeserializeObject<List<AtendimentoDTO>>(json);
            }

            return View(historico);
        }

        public ActionResult Create()
        {
            CarregarViewBagPacientes();
            return View(new AtendimentoDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AtendimentoDTO atendimento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiUrl = GetApiBaseUrl() + "atendimento";
                    var json = JsonConvert.SerializeObject(atendimento);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = _httpClient.PostAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                    var errorJson = response.Content.ReadAsStringAsync().Result;
                    var error = JsonConvert.DeserializeAnonymousType(errorJson, new { Message = "" });
                    ModelState.AddModelError(string.Empty, error?.Message ?? "Erro ao registrar o atendimento.");
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
            var apiUrl = GetApiBaseUrl() + "paciente";
            var response = _httpClient.GetAsync(apiUrl).Result;
            var pacientes = new List<PacienteDTO>();

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                pacientes = JsonConvert.DeserializeObject<List<PacienteDTO>>(json);
            }

            ViewBag.PacienteId = new SelectList(pacientes, "Id", "Nome");
        }
    }
}