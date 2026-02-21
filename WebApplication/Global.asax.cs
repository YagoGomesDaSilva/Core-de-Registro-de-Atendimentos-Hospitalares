using CrossCutting.DependenciesApp;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using SimpleInjector.Integration.WebApi;
using System.Web.Routing;

namespace WebApplication
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            DiConfigScopedWebApi();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void DiConfigScopedWebApi()
        {
            // 1. Cria o container
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            // 2. Chama a classe do CrossCutting.IoC para registrar os serviços
            NativeInjectorBootStrapper.RegisterServices(container);
            // 3. Registra os Controllers da Web API no container
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            // 4. Substitui o resolvedor padrão do .NET pelo Simple Injector
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

        }
    }
}
