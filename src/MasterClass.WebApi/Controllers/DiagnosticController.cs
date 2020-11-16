using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterClass.Core.Class;
using MasterClass.WebApi.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MasterClass.WebApi.Controllers
{
    [Route("api/_system")]
    public class DiagnosticController : Controller
    {
        private readonly IApplicationRequestContext _requestContext;
        private readonly DiagnosticOptions _options;

        public DiagnosticController(IApplicationRequestContext requestContext, IOptionsMonitor<DiagnosticOptions> options)
        {
            _requestContext = requestContext;
            _options = options.CurrentValue;
        }

        [HttpGet, HttpHead, Route("healthcheck")]
        public IActionResult HealthCheck()
        {
            ControllerContext.HttpContext.Response.Headers.Add("X-Guid2", _requestContext.Id.ToString());
            return Ok(_options.HealthCheckContent);

        }

    }
}
