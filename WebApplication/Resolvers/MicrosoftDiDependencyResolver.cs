using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace WebApplication.Resolvers
{
    public class MicrosoftDiDependencyResolver : IDependencyResolver
    {
        protected IServiceProvider _serviceProvider;

        public MicrosoftDiDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceProvider.GetServices(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            // Importante para gerenciar o tempo de vida (Scoped) a cada requisição HTTP
            var scope = _serviceProvider.CreateScope();
            return new MicrosoftDiDependencyResolver(scope.ServiceProvider);
        }

        public void Dispose()
        {
            // Libera a memória quando a requisição acaba
        }
    }
}