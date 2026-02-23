using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Application.DTO;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class AtendimentoController : BaseController
    {
        public ActionResult Index()
        {
            try
            {
                var apiUrl = GetApiBaseUrl() + "atendimento";
                var response = HttpClient.GetAsync(apiUrl).Result;
                var historico = new List<AtendimentoDTO>();

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    historico = JsonConvert.DeserializeObject<List<AtendimentoDTO>>(json);
                }

                return View(historico);
            }
            catch (Exception)
            {
                TempData["Erro"] = "Não foi possível carregar o histórico de atendimentos.";
                return View(new List<AtendimentoDTO>());
            }
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
                    var response = HttpClient.PostAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                    AdicionarErrosDaApi(response);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao registrar o atendimento.");
                }
            }

            CarregarViewBagPacientes();
            return View(atendimento);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var apiUrl = GetApiBaseUrl() + "atendimento/" + id;
                var response = HttpClient.GetAsync(apiUrl).Result;

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Erro"] = "Atendimento não encontrado.";
                    return RedirectToAction("Index");
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var atendimento = JsonConvert.DeserializeObject<AtendimentoDTO>(json);
                return View(atendimento);
            }
            catch (Exception)
            {
                TempData["Erro"] = "Não foi possível carregar os dados do atendimento.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AtendimentoDTO atendimento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiUrl = GetApiBaseUrl() + "atendimento/" + atendimento.Id;
                    var json = JsonConvert.SerializeObject(atendimento);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = HttpClient.PutAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Sucesso"] = "Atendimento atualizado com sucesso.";
                        return RedirectToAction("Index");
                    }

                    AdicionarErrosDaApi(response);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao atualizar o atendimento.");
                }
            }
            return View(atendimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var apiUrl = GetApiBaseUrl() + "atendimento/" + id;
                var response = HttpClient.DeleteAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["Sucesso"] = "Atendimento removido com sucesso.";
                }
                else
                {
                    TempData["Erro"] = ExtrairMensagemDeErro(response, "Erro ao excluir o atendimento.");
                }
            }
            catch (Exception)
            {
                TempData["Erro"] = "Ocorreu um erro inesperado ao excluir o atendimento.";
            }

            return RedirectToAction("Index");
        }

        private void CarregarViewBagPacientes()
        {
            try
            {
                var apiUrl = GetApiBaseUrl() + "paciente";
                var response = HttpClient.GetAsync(apiUrl).Result;
                var pacientes = new List<PacienteDTO>();

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    pacientes = JsonConvert.DeserializeObject<List<PacienteDTO>>(json);
                }

                ViewBag.PacienteId = new SelectList(pacientes, "Id", "Nome");
            }
            catch (Exception)
            {
                ViewBag.PacienteId = new SelectList(new List<PacienteDTO>(), "Id", "Nome");
            }
        }
    }
}