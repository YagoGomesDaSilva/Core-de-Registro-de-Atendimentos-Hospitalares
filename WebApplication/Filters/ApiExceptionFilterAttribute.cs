using Application.Exceptions;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebApplication.Filters
{
    /// <summary>
    /// Filtro global que centraliza o tratamento de exceções da Web API.
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;

            // 404 — entidade não encontrada
            if (exception is NotFoundException notFound)
            {
                context.Response = context.Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    notFound.Message);
                return;
            }

            // 400 — violação de regra de negócio / validação de serviço
            if (exception is ServiceException serviceEx)
            {
                context.Response = context.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    serviceEx.Message);
                return;
            }

            // 500 — erro inesperado (nunca expõe detalhes internos)
            Trace.TraceError(
                "[API ERROR] {0} {1} — {2}: {3}",
                context.Request.Method,
                context.Request.RequestUri,
                exception.GetType().Name,
                exception.Message);

            context.Response = context.Request.CreateErrorResponse(
                HttpStatusCode.InternalServerError,
                "Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}