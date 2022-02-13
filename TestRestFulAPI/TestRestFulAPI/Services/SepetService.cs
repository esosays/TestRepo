using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;

namespace TestRestFulAPI.Services
{
    public class SepetService : ISepetService
    {

        private readonly ISepetRepo Gnl_sepetRepo;
        private readonly ILogger<SepetService> Gnl_logger;

        public SepetService(ISepetRepo _sepetRepo, ILogger<SepetService> _logger)
        {
            Gnl_sepetRepo = _sepetRepo;
            Gnl_logger = _logger;
        }

        public void SepeteEkle(int musteriID, Sepet _sepet, Urun _urun)
        {
            _sepet.SepeteEkle(_urun);
            Gnl_sepetRepo.SepetGuncelle(musteriID, _sepet);
        }

        public void SepettenSil(int musteriID, Sepet _sepet, Urun _urun)
        {
            _sepet.SepettenSil(_urun.UrunID);
            Gnl_sepetRepo.SepetGuncelle(musteriID, _sepet);
        }

        //public bool SepetUrunMiktarGuncelle(int musteriID, Sepet _sepet, Urun _urun, int _degisenMiktar, out ICollection<ValidationResult> validationResults)
        //{
        //    //var isValid = basket.TryUpdateBasketItemQuantity(item.ProductId, newQuantity, out validationResults);
        //    var isValid = _sepet.SepetUrunMiktarGuncelle(_urun.UrunID, _degisenMiktar, out validationResults);

        //    if (isValid)
        //    {
        //        Gnl_sepetRepo.SepetGuncelle(musteriID, _sepet);
        //    }
        //    return isValid;
        //}
    }
}
