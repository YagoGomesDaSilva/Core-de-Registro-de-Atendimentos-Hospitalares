using Newtonsoft.Json;
using System.Web.Http;
using WebApplication.Filters;

namespace WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Filtro global de exceções — centraliza todo o tratamento de erros da API
            config.Filters.Add(new ApiExceptionFilterAttribute());

            // Rotas de API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = ReferenceLoopHandling.Ignore;
        }
    }
}
