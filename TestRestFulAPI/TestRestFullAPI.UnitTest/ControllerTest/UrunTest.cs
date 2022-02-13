using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;
using TestContext = TestRestFullAPI.UnitTest.Setup.TestContext;
using System.Net;

namespace TestRestFullAPI.UnitTest.ControllerTest
{
    [TestClass]
    public class UrunTest : IDisposable
    {

        private readonly TestContext _context;

        public UrunTest()
        {
            _context = new TestContext();
        }

        [TestMethod]
        public async Task UrunleriCek()
        {
            var response = await GetUrunlerAsync();
            var result = response.Content.ReadAsStringAsync().Result;
            response.EnsureSuccessStatusCode();
        }



        private async Task<HttpResponseMessage> GetUrunlerAsync() =>
            await _context.Client.GetAsync(new Uri($"/api/urun", UriKind.Relative));




        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        [TestCleanup]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
