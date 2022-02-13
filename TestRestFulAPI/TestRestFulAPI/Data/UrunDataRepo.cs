using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;

namespace TestRestFulAPI.Data
{
    public class UrunDataRepo : IUrunRepo
    {
        private readonly IDataDBContext _datadbContext;
        private readonly ILogger<UrunDataRepo> _logger;


        public UrunDataRepo(IDataDBContext datadbContext, ILogger<UrunDataRepo> logger)
        {
            _datadbContext = datadbContext;
            _logger = logger;
        }

        public void UrunEkle(Urun urun)
        {
            _datadbContext.DBUrun.Add(urun);
        }

        public ICollection<Urun> UrunGetir()
        {
            return _datadbContext.DBUrun.ToList();
        }

        public bool UrunKontrolu(string _urunID, out Urun _urun)
        {
            _urun = _datadbContext.DBUrun.FirstOrDefault(c=> c.UrunID == _urunID);

            if (_urun == null)
            {
                _logger.LogWarning($"Tanımlı urun bulunamadı : {_urun}");
                return false;
            }

            _logger.LogInformation($"Urun Kayitli : {_urun}");
            return true;


        }

        /*public bool UrunMiktarGuncelle(string urunID, int satilanMiktar, out ICollection<ValidationResult> validationResults)
        {
            var varmi = _datadbContext.DBUrun.Single(c => c.UrunID == urunID);
            if (varmi != null)
            {
                varmi.Miktar -= satilanMiktar;
            }
            return true;
        }*/

        public bool UrunMiktarGuncelle(Urun _urun, int _degisenMiktar)
        {
            var varmi = _datadbContext.DBUrun.Single(c => c.UrunID == _urun.UrunID);
            if (varmi != null)
            {
                varmi.Miktar -= _degisenMiktar;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
