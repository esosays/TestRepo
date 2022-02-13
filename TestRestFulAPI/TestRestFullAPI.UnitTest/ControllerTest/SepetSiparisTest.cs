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
    public class SepetSiparisTest : IDisposable
    {

        private readonly TestContext _context;

        public SepetSiparisTest()
        {
            _context = new TestContext();
        }

        [TestMethod]
        public async Task SepetOlustur()
        {
            int musteriID = 4;
            Sepet sepet = new Sepet();
            sepet.SepeteEkle(new Urun("P1", "Padi", 44, 2));

            var response = await PostSepet(musteriID, sepet);
            var result = response.Content.ReadAsStringAsync().Result;
            response.EnsureSuccessStatusCode();
        }
        [TestMethod]
        public async Task SiparisOlustur()
        {
            int musteriID = 4;
            Sepet sepet = new Sepet();
            sepet.SepeteEkle(new Urun("P1", "Padi", 44, 2));

            var response = await PostSepet(musteriID, sepet);
            var result = response.Content.ReadAsStringAsync().Result;
            response.EnsureSuccessStatusCode();

            var response_siparis = await PostSiparis(musteriID);
            if(response_siparis.StatusCode == HttpStatusCode.NotFound)
            {
                // sepet bulunamadı. önce sepet oluşturulmalı
            }
        }


        private async Task<HttpResponseMessage> PostSepet(int musteriID, Sepet sepet) =>
            await _context.Client.PostAsJsonAsync($"/api/sepet/{musteriID}", sepet);

        private async Task<HttpResponseMessage> PostSiparis(int musteriID) =>
            await _context.Client.PostAsJsonAsync($"/api/siparis/{musteriID}", "");




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
