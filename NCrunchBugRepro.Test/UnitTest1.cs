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
        private const string ControllerUrl = "/";

        [Fact]
        public async Task TestHost_Does_Not_Work_In_NCrunch()
        {
            var server = new TestServer(new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseMvc();
                })
                .ConfigureServices(services =>
                {
                    services.AddMvc();
                }));

            var client = server.CreateClient();

            using (var response = await client.GetAsync(ControllerUrl))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(response.IsSuccessStatusCode);
            }
        }
    }

}
