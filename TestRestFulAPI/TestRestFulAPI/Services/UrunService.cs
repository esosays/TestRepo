using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;

namespace TestRestFulAPI.Services
{
    public class UrunService : IUrunService
    {
        private readonly IUrunRepo Gnl_UrunRepo;
        private readonly ILogger<UrunService> Gnl_logger;

        public UrunService(IUrunRepo _urunRepo, ILogger<UrunService> _logger)
        {
            Gnl_UrunRepo = _urunRepo;
            Gnl_logger = _logger;
        }

        public void UrunEkle(Urun urun)
        {
            Gnl_UrunRepo.UrunEkle(urun);
        }

        public void UrunKontrolu(string _urunID, out Urun _urun)
        {
            Gnl_UrunRepo.UrunKontrolu(_urunID, out _urun);
        }

        public bool UrunMiktarGuncelle(Urun _urun, int _degisenMiktar)
        {
            var isValid = Gnl_UrunRepo.UrunMiktarGuncelle(_urun, _degisenMiktar);
            return isValid;
        }

        public decimal UrunMiktarKontrolu(string _urunID, out Urun urun)
        {
            var isValid = Gnl_UrunRepo.UrunKontrolu(_urunID, out urun);
            if (urun != null)
            {

            }
            return -11;
        }
    }
}
