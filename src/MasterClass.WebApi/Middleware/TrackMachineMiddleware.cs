using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterClass.WebApi.Middleware
{
    public class TrackMachineMiddleware
    {
        private readonly RequestDelegate _next;

        public TrackMachineMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Machine", Environment.MachineName);
            await _next(context);
        }
    }
}
