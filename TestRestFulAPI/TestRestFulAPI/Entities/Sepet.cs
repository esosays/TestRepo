using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestFulAPI.Entities
{
    public class Sepet
    {

        //[Required]
        //[Range(1, 100)]
        //public int MusteriID { get; set; }

        public List<Urun> SepetUrunler => _sepetUrunler;
        private List<Urun> _sepetUrunler { get; set; }

        public Sepet()
        {
            _sepetUrunler = new List<Urun>();
        }

        public bool SepetUrunKontrolu(string _urunID)
        {
            return _sepetUrunler.Any(c=> c.UrunID == _urunID);
        }

        public void SepeteEkle(Urun _urun)
        {
            if (!SepetUrunKontrolu(_urun.UrunID))
            {
                _sepetUrunler.Add(_urun);
            }
        }
        public void SepettenSil(string _urunID)
        {
            if (SepetUrunKontrolu(_urunID))
            {
                var basketItem = _sepetUrunler.Single(c=> c.UrunID == _urunID);
                _sepetUrunler.Remove(basketItem);
            }
        }

        public bool SepetUrunMiktarGuncelle(string _urunID, int _degisenMiktar, out ICollection<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            if (SepetUrunKontrolu(_urunID))
            {
                var _urun = _sepetUrunler.Single(c=> c.UrunID == _urunID);
                //basketItem.UpdateQuantity(_degisenMiktar);
                _urun.MiktarGuncelle(_degisenMiktar);

                var context = new ValidationContext(_urun);
                var isValid = Validator.TryValidateObject(_urun, context, validationResults, true);

                return isValid;
            }
            validationResults.Add(new ValidationResult("Mevcut sepette ürün bulunamadı."));
            return false;
        }

        public void UpdatePriceOfProductInBasket(string _urunID, decimal _degisenFiyat)
        {
            if (SepetUrunKontrolu(_urunID))
            {
                var _urun = _sepetUrunler.Single(c => c.UrunID == _urunID);
                _urun.FiyatGuncelle(_degisenFiyat);
            }
        }
    }
}
