using FactorialService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;

namespace FactorialService {
    public class Startup {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ServiceClient<LogRequest, LogResponse>>(new ServiceClient<LogRequest,LogResponse>("http://localhost:5001", "log"));
        }
        public void Configure (IApplicationBuilder app, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole ();

            app.UseOwin (pipeline => pipeline.UseNancy (options => {
                options.Bootstrapper = new Bootstrapper (app.ApplicationServices);
            }));
        }
    }
}