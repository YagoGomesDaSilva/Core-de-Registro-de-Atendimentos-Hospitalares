using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Controller base que centraliza a infraestrutura comum de comunicação com a API.
    /// Elimina duplicação de HttpClient, construção de URL e tratamento de erros da API.
    /// </summary>
    public abstract class BaseController : Controller
    {
        protected static readonly HttpClient HttpClient = new HttpClient();

        /// <summary>
        /// Constrói a URL base da API a partir da requisição atual.
        /// </summary>
        protected string GetApiBaseUrl()
        {
            return string.Format("{0}://{1}{2}api/",
                Request.Url.Scheme, Request.Url.Authority, Url.Content("~/"));
        }

        /// <summary>
        /// Extrai a mensagem de erro da resposta da API e adiciona ao ModelState.
        /// </summary>
        protected void AdicionarErrosDaApi(HttpResponseMessage response)
        {
            var mensagem = ExtrairMensagemDeErro(response, "Erro ao processar a solicitação.");
            ModelState.AddModelError(string.Empty, mensagem);
        }

        /// <summary>
        /// Extrai a mensagem de erro do corpo da resposta HTTP.
        /// </summary>
        protected static string ExtrairMensagemDeErro(HttpResponseMessage response, string fallback)
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