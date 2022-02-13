using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;

namespace TestRestFulAPI.Data
{
    public class SiparisDataRepo : ISiparisRepo
    {

        private readonly IDataDBContext _datadbContext;
        private readonly ILogger<SiparisDataRepo> _logger;


        public SiparisDataRepo(IDataDBContext datadbContext, ILogger<SiparisDataRepo> logger)
        {
            _datadbContext = datadbContext;
            _logger = logger;
        }

        public Siparis SiparisEkle(int musteriID, out Siparis _siparis, out string RetAck)
        {
            var kontrol = _datadbContext.DBSepet.TryGetValue(musteriID, out Sepet out_sepet);

            if (kontrol == false)
            {
                _logger.LogWarning("Önce Sepet Oluşturunuz {0}", musteriID);
                RetAck = $"Önce Sepet Oluşturunuz {musteriID}";
                _siparis = null;
                return _siparis;
            }
            else
            {

                // ayni musteriye aynı ürün satılamaz;
                List<string> Sepet_UrunIDList = out_sepet.SepetUrunler.Select(c => c.UrunID.ToLower()).ToList();

                var Musteribazli_Siparisler = _datadbContext.DBSiparis.Values.Where(c => c.MusteriID == musteriID).ToList();
                List<string> DahaOnceSatilanIDler = Musteribazli_Siparisler.SelectMany(c => c.SatilanUrunler.Where(t => t.UrunID != "")).Select(c => c.UrunID.ToLower()).ToList();



                var karsilastirmaSonucu = DahaOnceSatilanIDler.Where(c => Sepet_UrunIDList.Contains(c.ToLower())).ToList();
                if (karsilastirmaSonucu.Count > 0)
                {
                    // daha once satilmiş
                    _logger.LogWarning("Daha önce satılmış {0}", musteriID);
                    RetAck = $"Daha önce satılmış {musteriID}";
                    _siparis = null;
                    return _siparis;
                }
                else
                {
                    // var ret_varmi = _datadbContext.DBSiparis.TryGetValue(musteriID, out _siparis);
                    //if (!ret_varmi)

                    RetAck = "";
                    var yeni_siparis = new Siparis();
                    yeni_siparis.MusteriID = musteriID;
                    yeni_siparis.SiparisID = Guid.NewGuid();
                    yeni_siparis.SiparisTarihi = DateTime.Now;
                    yeni_siparis.SatilanUrunler = out_sepet.SepetUrunler;

                    _datadbContext.DBSiparis.TryAdd(yeni_siparis.SiparisID, yeni_siparis);
                    _logger.LogWarning("Sipariş oluşturuldu {0}", musteriID);

                    _datadbContext.DBSepet.Remove(musteriID);

                    _siparis = yeni_siparis;
                    return _siparis;
                }
            }
        }

        public List<Siparis> SiparisGetir(int _musteriID)
        {
            var ret_varmi = _datadbContext.DBSiparis.Where(c => c.Value.MusteriID == _musteriID).Select(t => t.Value).ToList();

            if (ret_varmi.Count == 0)
            {
                _logger.LogWarning("Siparis Bulunamadı {0}", _musteriID);
            }
            else
            {
                _logger.LogInformation("Siparis bulundu: {0}", _musteriID);
            }
            return ret_varmi.ToList();
        }

        public List<Siparis> SiparisGetirHepsi()
        {
            var ret_varmi = _datadbContext.DBSiparis.Select(t => t.Value).ToList();

            if (ret_varmi.Count == 0)
            {
                _logger.LogWarning("Siparis Bulunamadı");
            }
            else
            {
                _logger.LogInformation("Siparis bulundu: {0}");
            }
            return ret_varmi.ToList();
        }

        public bool SiparisUrunKontrolu(string _urunID, int _musteriID, out Siparis _siparis)
        {
            throw new NotImplementedException();
        }
    }
}
