using MasterClass.Service.Implementation;
using MasterClass.Service.Interface.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterClass.Service.DependencyInjection.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();

            return services;
        }
    }
}
