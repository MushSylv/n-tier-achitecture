using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterClass.Business.DependencyInjection.Extensions;
using MasterClass.Core.Class;
using MasterClass.Repository.DependencyInjection.Extensions;
using MasterClass.Service.DependencyInjection.Extensions;
using MasterClass.WebApi.DependencyInjection.Extensions;
using MasterClass.WebApi.Implementation;
using MasterClass.WebApi.Interface;
using MasterClass.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MasterClass.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureMock(Configuration);

            services.AddRepository();
            services.AddBusiness();
            services.AddService();
            services.AddMasterClassSwagger();
            services.AddMasterClassAuthentication(Configuration);
            services.AddMasterClassAuthorization();
            services.AddControllers();
            services.AddSingleton<IApplicationRequestContext, ApplicationRequestContext>();
            services.Configure<DiagnosticOptions>(Configuration.GetSection("Diagnostic"));
            //services.AddScoped<IApplicationRequestContext, ApplicationRequestContext>();
            //services.AddTransient<IApplicationRequestContext, ApplicationRequestContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMiddleware<TrackMachineMiddleware>(); 
            app.UseMiddleware<TrackRequestContextMiddleware>();
            app.UseMasterClassSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
