using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net.Http;
using Xunit.Abstractions;

namespace CashBack.Integration.Tests
{
    public class TestServerFixture : WebApplicationFactory<CashBack.Web.Startup>
    {
        public HttpClient Client { get; }
        public ITestOutputHelper Output { get; set; }

        protected override IHostBuilder CreateHostBuilder()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var builder = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    //logging.AddXunit(Output);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureTestServices((services) =>
                        {
                            services
                                .AddControllers()
                                .AddApplicationPart(typeof(CashBack.Web.Startup).Assembly);
                        });
                });

            return builder;
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<CashBack.Web.Startup>();
            base.ConfigureWebHost(builder);
        }

        public TestServerFixture SetOutPut(ITestOutputHelper output)
        {
            Output = output;
            return this;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Output = null;
        }
    }
}
