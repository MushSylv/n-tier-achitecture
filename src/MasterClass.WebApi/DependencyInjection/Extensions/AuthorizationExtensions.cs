using MasterClass.Service.Identity;
using MasterClass.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterClass.WebApi.DependencyInjection.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddMasterClassAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.REQUIRED_SUPERADMIN_ROLE, policy =>
                    policy.RequireRole("Admin")
                        .RequireClaim(MasterClassClaims.RIGHTS_CLAIMNAME, "SuperAdmin"));
                options.AddPolicy(Policies.REQUIRED_ADMIN_ROLE, policy => policy.RequireRole("Admin"));
                options.AddPolicy(Policies.REQUIRED_ALCOHOL_MAJORITY, policy => policy.Requirements.Add(new MinimumAgeRequirement(18)));
            })
            .AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            return services;
        }
    }
}
