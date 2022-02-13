using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace TestRestFulAPI.Services
{
    public interface ISepetService
    {
        void SepeteEkle(int musteriID,Sepet sepet, Urun _urun);
        void SepettenSil(int musteriID,Sepet sepet, Urun _urun);
        //bool SepetUrunMiktarGuncelle(int musteriID, Sepet sepet, Urun _urun, int _degisenMiktar, out ICollection<ValidationResult> validationResults);
    }
}
