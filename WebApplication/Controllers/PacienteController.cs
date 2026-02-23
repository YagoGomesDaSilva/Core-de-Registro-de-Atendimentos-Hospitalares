using Application.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class PacienteController : Controller
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private string GetApiBaseUrl()
        {
            return string.Format("{0}://{1}{2}api/",
                Request.Url.Scheme, Request.Url.Authority, Url.Content("~/"));
        }

        public ActionResult Index()
        {
            try
            {
                var apiUrl = GetApiBaseUrl() + "paciente";
                var response = _httpClient.GetAsync(apiUrl).Result;
                var pacientes = new List<PacienteDTO>();

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    pacientes = JsonConvert.DeserializeObject<List<PacienteDTO>>(json);
                }

                return View(pacientes);
            }
            catch (Exception)
            {
                TempData["Erro"] = "Não foi possível carregar a lista de pacientes.";
                return View(new List<PacienteDTO>());
            }
        }

        public ActionResult Create()
        {
            return View(new PacienteDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PacienteDTO paciente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiUrl = GetApiBaseUrl() + "paciente";
                    var json = JsonConvert.SerializeObject(paciente);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = _httpClient.PostAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                    AdicionarErrosDaApi(response);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao salvar o paciente.");
                }
            }
            return View(paciente);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var apiUrl = GetApiBaseUrl() + "paciente/" + id;
                var response = _httpClient.GetAsync(apiUrl).Result;

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Erro"] = "Paciente não encontrado.";
                    return RedirectToAction("Index");
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var paciente = JsonConvert.DeserializeObject<PacienteDTO>(json);
                return View(paciente);
            }
            catch (Exception)
            {
                TempData["Erro"] = "Não foi possível carregar os dados do paciente.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PacienteDTO paciente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiUrl = GetApiBaseUrl() + "paciente/" + paciente.Id;
                    var json = JsonConvert.SerializeObject(paciente);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = _httpClient.PutAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Sucesso"] = "Paciente atualizado com sucesso.";
                        return RedirectToAction("Index");
                    }

                    AdicionarErrosDaApi(response);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro inesperado ao atualizar o paciente.");
                }
            }
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var apiUrl = GetApiBaseUrl() + "paciente/" + id;
                var response = _httpClient.DeleteAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["Sucesso"] = "Paciente removido com sucesso.";
                }
                else
                {
                    TempData["Erro"] = ExtrairMensagemDeErro(response, "Erro ao excluir o paciente.");
                }
            }
            catch (Exception)
            {
                TempData["Erro"] = "Ocorreu um erro inesperado ao excluir o paciente.";
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Extrai a mensagem de erro da resposta da API e adiciona ao ModelState.
        /// </summary>
        private void AdicionarErrosDaApi(HttpResponseMessage response)
        {
            var mensagem = ExtrairMensagemDeErro(response, "Erro ao processar a solicitação.");
            ModelState.AddModelError(string.Empty, mensagem);
        }

        /// <summary>
        /// Extrai a mensagem de erro do corpo da resposta HTTP.
        /// </summary>
        private static string ExtrairMensagemDeErro(HttpResponseMessage response, string fallback)
        {
            try
            {
                var errorJson = response.Content.ReadAsStringAsync().Result;
                var error = JsonConvert.DeserializeAnonymousType(errorJson, new { Message = "" });
                return error?.Message ?? fallback;
            }
            catch
            {
                return fallback;
            }
        }
    }
}