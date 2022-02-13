using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;

namespace TestRestFulAPI.Data
{
    public class SepetDataRepository : ISepetRepo
    {


        private readonly IDataDBContext _datadbContext;
        private readonly ILogger<SepetDataRepository> _logger;


        public SepetDataRepository(IDataDBContext datadbContext, ILogger<SepetDataRepository> logger)
        {
            _datadbContext = datadbContext;
            _logger = logger;
        }

        public ICollection<Sepet> SepetGetir()
        {
            return _datadbContext.DBSepet.Values;
        }


        public bool SepetGetir(int _musteriID, out Sepet _sepet)
        {
            var ret_varmi = _datadbContext.DBSepet.TryGetValue(_musteriID, out _sepet);

            if (!ret_varmi)
            {
                _logger.LogWarning("{0} müşteriye ait sepet bulunamadı", _musteriID);
            }
            else
            {
                _logger.LogInformation("{0} müşterinin sepeti çekildi", _musteriID);
            }
            return ret_varmi;
        }

        public Sepet SepetGuncelle(int musteriID, Sepet _sepet)
        {



            var sepet_urunKontrol = _datadbContext.DBUrun.Where(c => _sepet.SepetUrunler.Select(t => t.UrunID.ToLower()).Contains(c.UrunID.ToLower())).ToList();
            if (sepet_urunKontrol.Count != _sepet.SepetUrunler.Count)
            {
                _logger.LogInformation("Tanımsız ürün sepete eklenemez");
                return null;
            }


            var ret_varmi = _datadbContext.DBSepet[musteriID] = _sepet;
            if (ret_varmi != null)
            {
                _logger.LogInformation("Sepet Ekleme başarılı {0}", musteriID);
            }
            else
            {
                _logger.LogWarning("Sepet ekleme başarısız {0}", musteriID);
            }
            return ret_varmi;
        }

        public void SepetSil(int _musteriID)
        {
            _datadbContext.DBSepet.Remove(_musteriID);
        }

        public bool SepetUrunKontrolu(string _urunID, int _musteriID, out Urun _urun)
        {
            _urun = _datadbContext.DBSepet[_musteriID].SepetUrunler.FirstOrDefault(c => c.UrunID == _urunID);

            if (_urun == null)
            {
                _logger.LogWarning($"{_urunID} ürün {_musteriID} sepetinde bulunamadı");
                return false;
            }

            _logger.LogInformation($"{_urunID} ürün {_musteriID} sepetinde mevcut");
            return true;
        }
    }
}
