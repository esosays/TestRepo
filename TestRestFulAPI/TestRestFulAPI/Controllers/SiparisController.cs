using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;
using TestRestFulAPI.Services;

namespace TestRestFulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiparisController : Controller
    {

        private readonly ISepetRepo Gnl_SepetRepo;
        private readonly ISiparisRepo Gnl_siparisRepo;
        private readonly IUrunRepo Gnl_UrunRepo;

        private readonly ILogger<SiparisController> Gnl_logger;


        public SiparisController(ISiparisRepo _siparisRepo, IUrunRepo _urunRepo, ISepetRepo _sepetRepo, ILogger<SiparisController> _logger)
        {
            Gnl_siparisRepo = _siparisRepo;
            Gnl_logger = _logger;
            Gnl_UrunRepo = _urunRepo;
            Gnl_SepetRepo = _sepetRepo;

        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Siparis>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<List<Siparis>> GetTumu()
        {
            var tumu = Gnl_siparisRepo.SiparisGetirHepsi();
            if (tumu.Count == 0)
            {
                return SiparisBulunamadiHepsi();
            }

            return tumu;
        }

        [HttpGet("{musteriID}")]
        [ProducesResponseType(typeof(List<Siparis>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<List<Siparis>> Get(int musteriID)
        {
            var tumu = Gnl_siparisRepo.SiparisGetir(musteriID);
            if (tumu.Count == 0)
            {
                return SiparisBulunamadi(musteriID);
            }

            return tumu;
        }

        [HttpPost("{musteriID}")]
        [ProducesResponseType(typeof(Siparis), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Siparis> Post(int musteriID)
        {
            try
            {

                var sepet_sonuc = Gnl_SepetRepo.SepetGetir(musteriID, out Sepet sepet);
                if (sepet == null)
                {
                    return SepetBulunamadi("", musteriID);
                }

                List<string> ret_urun_message = new List<string>();

                foreach (var item in sepet.SepetUrunler)
                {
                    Gnl_UrunRepo.UrunKontrolu(item.UrunID, out Urun urun);
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
                        if (urun.Miktar == 0)
                            ret_urun_message.Add($"{urun.UrunAdi} ürün tükenmiştir.");
                        else
                            ret_urun_message.Add($"{urun.UrunAdi} üründen en fazla {urun.Miktar} sepete eklenebilir.");
                    }
                }

                if (ret_urun_message.Count > 0)
                {
                    return YetersizMiktar(string.Join(Environment.NewLine, ret_urun_message));
                }


                var siparis_sonuc = Gnl_siparisRepo.SiparisEkle(musteriID, out Siparis siparis, out string RetAck);
                if (siparis_sonuc == null) // sepet yok.
                {

                    return SepetBulunamadi(RetAck, musteriID);
                }
                else
                {
                    foreach (var item in siparis.SatilanUrunler)
                    {
                        Gnl_UrunRepo.UrunMiktarGuncelle(item, item.Miktar);
                    }


                    return CreatedAtRoute("Get", new
                    {
                        MusteriID = siparis_sonuc.MusteriID
                    }, siparis_sonuc);
                }


            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        private ActionResult YetersizMiktar(string _message)
        {
            var message = _message;
            Gnl_logger.LogWarning(message);
            return NotFound(message);
        }

        private ActionResult SepetBulunamadi(string msj, int musteriID)
        {
            string message = $"{musteriID} ait sepet bulunamadı.";
            if (!string.IsNullOrEmpty(msj))
                message = msj;

            Gnl_logger.LogWarning(message);
            return NotFound(message);
        }

        private ActionResult SiparisBulunamadi(int musteriID = 0)
        {
            var message = $"{musteriID} ait sipariş bulunamadı.";
            Gnl_logger.LogWarning(message);
            return NotFound(message);
        }
        private ActionResult SiparisBulunamadiHepsi()
        {
            var message = $"Hiç bir siparis bulunamadı.";
            Gnl_logger.LogWarning(message);
            return NotFound(message);
        }

    }
}
