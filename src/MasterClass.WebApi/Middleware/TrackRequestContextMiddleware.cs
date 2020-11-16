using MasterClass.WebApi.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterClass.WebApi.Middleware
{
    public class TrackRequestContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApplicationRequestContext _requestContext;

        public TrackRequestContextMiddleware(RequestDelegate next, IApplicationRequestContext requestContext)
        {
            _next = next;
            _requestContext = requestContext;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Guid", _requestContext.Id.ToString());
            await _next(context);
        }
    }
}
