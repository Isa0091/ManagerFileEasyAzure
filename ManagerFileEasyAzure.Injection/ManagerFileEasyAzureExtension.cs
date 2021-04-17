using ManagerFileEasyAzure;
using ManagerFileEasyAzure.Provider;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerFileEasyAzure.Injection
{
    public static class ManagerFileEasyAzureExtension
    {
        
        /// <summary>
        /// Agrega la injeccion de dependencias
        /// </summary>
        /// <param name="services"></param>
        public static void AddManegerFileEasyAzure(this IServiceCollection services,string connection, string container)
        {
            services.AddScoped<IManagerFileEasyAzureProvider>(x =>new ManagerFileEasyAzureProvider(connection, container));
        }
    }
}
