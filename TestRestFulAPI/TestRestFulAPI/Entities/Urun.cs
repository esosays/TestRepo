using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestFulAPI.Entities
{
    public class Urun
    {
        [Required]
        [StringLength(50)]
        public string UrunID { get; set; }

        [Required]
        [StringLength(100)]
        public string UrunAdi { get; set; }

        [Required]
        public decimal Fiyat { get; set; }


        [Required]
        [Range(1, 9, ErrorMessage = "{1} and {2} arasında olmalıdır.")]
        public int Miktar { get; set; }

        public Urun(string _urunID, string _urunAdi, decimal _fiyat, int _miktar)
        {
            UrunID = _urunID;
            UrunAdi = _urunAdi;
            Fiyat = _fiyat;
            Miktar = _miktar;
        }
        internal void FiyatGuncelle(decimal _degisenFiyat)
        {
            Fiyat = _degisenFiyat;
        }
        internal void MiktarGuncelle(int _degisenMiktar)
        {
            Miktar = _degisenMiktar;
        }
    }
}
