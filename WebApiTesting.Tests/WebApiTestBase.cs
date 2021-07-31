using NUnit.Framework;
using System.IO;
using System.Net.Http.Headers;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;

namespace WebApiTesting.Tests
{
    public abstract class WebApiTestBase
    {
        private readonly string settingsFilePath = Path.GetFullPath(Path.Combine("..", "..", "..", "..", "WebApiTesting"));

        private TestServer server;

        protected HttpClient Client { get; private set; }

        protected abstract string ApiRoute { get; }

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(settingsFilePath, "appsettings.json"), optional: true, reloadOnChange: true)
                .AddJsonFile(Path.Combine(settingsFilePath, "appsettings.Development.json"), optional: true, reloadOnChange: true)
                .Build();

            this.server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration));
            this.Client = this.server.CreateClient();

            this.Client.BaseAddress = new Uri(this.Client.BaseAddress.ToString() + this.ApiRoute);
        }
    }
}