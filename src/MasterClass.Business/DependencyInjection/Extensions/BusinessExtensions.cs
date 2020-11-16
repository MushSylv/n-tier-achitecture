using MasterClass.Business.Implementation;
using MasterClass.Business.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterClass.Business.DependencyInjection.Extensions
{
    public static class BusinessExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddSingleton<IUserBusiness, UserBusiness>();

            return services;
        }
    }
}
