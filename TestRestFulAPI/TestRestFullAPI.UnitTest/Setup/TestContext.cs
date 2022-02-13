using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TestRestFulAPI;

namespace TestRestFullAPI.UnitTest.Setup
{
    public class TestContext : IDisposable
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }

        public TestContext()
        {
            SetUpClient();
        }

        private void SetUpClient()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            _server = new TestServer(new WebHostBuilder()
                .UseConfiguration(config)
                .UseStartup<Startup>());

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("https://localhost:44334/");
        }

        
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _server?.Dispose();
                    Client?.Dispose();
                }
                _disposedValue = true;
            }
        }

        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
