using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MasterClass.WebApi.Test.Controllers.Fixtures
{
    public class DiagnosticControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public DiagnosticControllerTest(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.WithWebHostBuilder(builder => builder.UseEnvironment("ci")).CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        [InlineData("HEAD")]
        public async void HealthCheck_Get_Status200(string method)
        {
            var response = await _client.SendAsync(new HttpRequestMessage(new HttpMethod(method), "api/_system/healthcheck"));

            if (!HttpMethods.IsHead(method)
                && !HttpMethods.IsDelete(method)
                && !HttpMethods.IsTrace(method))
            {
                Assert.Equal("system_ok", await response.Content.ReadAsStringAsync());
            }

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
