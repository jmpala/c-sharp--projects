using LatinoNet.Presenters;
using LatinoNet.RepositoryEFCore;
using LatinoNet.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatinoNet.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddLatinoNetDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddUseCasesServices();
            services.AddPresenters();
            return services;
        }
    }
}
