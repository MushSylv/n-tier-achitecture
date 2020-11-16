using MasterClass.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterClass.Repository.DependencyInjection.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, MockUserRepository>();

            return services;
        }
    }
}
