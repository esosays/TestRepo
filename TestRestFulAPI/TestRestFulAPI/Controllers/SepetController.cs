using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;

namespace TestRestFulAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SepetController : Controller
    {
        private readonly ISepetRepo Gnl_sepetRepo;
        private readonly IUrunRepo Gnl_urunRepo;
        private readonly ILogger<SepetController> Gnl_logger;
        public SepetController(ISepetRepo _sepetRepo, IUrunRepo _urunRepo, ILogger<SepetController> _logger)
        {
            Gnl_sepetRepo = _sepetRepo;
            Gnl_urunRepo = _urunRepo;
            Gnl_logger = _logger;

        }
        [HttpGet("{musteriID}", Name = "Get")]
        [ProducesResponseType(typeof(Sepet), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Sepet> Get(int musteriID)
        {
            if (!Gnl_sepetRepo.SepetGetir(musteriID, out Sepet sepet))
            {
                return SepetBulunamadi(musteriID);
            }
            return sepet;
        }

        [HttpPost("{musteriID}")]
        [ProducesResponseType(typeof(Sepet), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Sepet> Post(int musteriID, [FromBody] Sepet sepet)
        {

            try
            {
                List<string> ret_urun_message = new List<string>();

                foreach (var item in sepet.SepetUrunler)
                {
                    Gnl_urunRepo.UrunKontrolu(item.UrunID, out Urun urun);
                    if (urun == null)
                    {
                        ret_urun_message.Add($"{item.UrunAdi} ürün tanımsız.");
                    }
                    else if (urun.Miktar >= item.Miktar)
                    {
                        /// sorun yok miktar tutarlı
                    }
                    else
                    {
                        if(urun.Miktar == 0)
                            ret_urun_message.Add($"{urun.UrunAdi} ürün tükenmiştir.");
                        else
                            ret_urun_message.Add($"{urun.UrunAdi} üründen en fazla {urun.Miktar} sepete eklenebilir.");
                    }
                }

                if (ret_urun_message.Count > 0)
                {
                    return YetersizMiktar(string.Join(Environment.NewLine, ret_urun_message));
                }

                var sepet_sonuc = Gnl_sepetRepo.SepetGuncelle(musteriID, sepet);

                if (sepet_sonuc == null)
                {
                    return TanimsizUrun(musteriID);
                }

                return CreatedAtRoute("Get", new
                {
                    MusteriID = musteriID
                }, sepet_sonuc);



            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [HttpDelete("{musteriID}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public ActionResult Delete(int musteriID)
        {
            if (!Gnl_sepetRepo.SepetGetir(musteriID, out Sepet sepet))
            {
                return SepetBulunamadi(musteriID);
            }

            Gnl_sepetRepo.SepetSil(musteriID);
            return NoContent();
        }
        private ActionResult YetersizMiktar(string _message)
        {
            var message = _message;
            Gnl_logger.LogWarning(message);
            return NotFound(message);
        }

        private ActionResult TanimsizUrun(int musteriID)
        {
            var message = $"{musteriID} sadece tanımlı ürünler sepete eklenebilir.";
            Gnl_logger.LogWarning(message);
            return NotFound(message);
        }

        private ActionResult SepetBulunamadi(int musteriID)
        {
            var message = $"{musteriID} ait sepet bulunamadı.";
            Gnl_logger.LogWarning(message);
            return NotFound(message);
        }

    }
}
