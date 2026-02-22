using System.IO;
using System.Web.Http;
using Swashbuckle.Application;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApplication.SwaggerConfig), "Register")]

namespace WebApplication
{
    /// <summary>
    /// Configuração do Swagger para documentação da API.
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Registra e configura o Swagger para a aplicação.
        /// </summary>
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "API de Registro de Atendimentos Hospitalares")
                     .Description("Documentação da API para gerenciamento de pacientes e atendimentos hospitalares.")
                     .Contact(cc => cc.Name("Suporte"));

                    var xmlPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin", "WebApplication.xml");
                    if (File.Exists(xmlPath))
                        c.IncludeXmlComments(xmlPath);
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("Swagger - Atendimentos Hospitalares");
                });
        }
    }
}