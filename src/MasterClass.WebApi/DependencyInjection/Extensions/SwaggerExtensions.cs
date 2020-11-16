using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterClass.WebApi.DependencyInjection.Extensions
{
    public static class SwaggerExtensions
    {
        private const string VERSION = "v1";
        private const string SWAGGER_DOCNAME = "masterclass.webapi." + VERSION;

        public static IServiceCollection AddMasterClassSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(genOptions
                =>
            {
                genOptions.SwaggerDoc(SWAGGER_DOCNAME,
                        new OpenApiInfo
                        {
                            Title = "MasterClass WebApi",
                            Version = VERSION
                        });
                genOptions.AddJwtBearerSecurity();
            });

            return services;
        }

        public static IApplicationBuilder UseMasterClassSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(uiOptions => uiOptions.SwaggerEndpoint($"/swagger/{SWAGGER_DOCNAME}/swagger.json", "MasterClass WebApi"));

            return app;
        }

        private static void AddJwtBearerSecurity(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition(
                JwtBearerDefaults.AuthenticationScheme,
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header
                });
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
            {
                new OpenApiSecurityScheme { Scheme = JwtBearerDefaults.AuthenticationScheme },
                new string[] {}
            }
                });
        }
    }
}
