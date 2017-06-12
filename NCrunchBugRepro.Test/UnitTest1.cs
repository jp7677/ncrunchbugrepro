using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace NCrunchBugRepro.Test
{
    public class TestHostFailure
    {
        private TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

        [Fact]
        public async Task WebApiCallShouldResponseWithOk()
        {
            using (var client = server.CreateClient())
            using (var response = await client.GetAsync("/"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(response.IsSuccessStatusCode);
            }
        }

        /// <summary>
        /// This test fails with ncrunch 3.9.0.1, but succeeds on the command line with "dotnet test"
        /// The solution for ncrunch to succeed is to copy the NCrunchBugRepro\bin\Debug\netcoreapp1.1\NCrunchBugRepro.deps.json into the ncrunch folder for that test case
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task MvcViewShouldResponseWithOk()
        {
            using (var client = server.CreateClient())
            using (var response = await client.GetAsync("/Home"))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(response.IsSuccessStatusCode);
            }
        }
    }
}
