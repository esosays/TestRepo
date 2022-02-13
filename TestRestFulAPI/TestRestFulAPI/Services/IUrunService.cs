using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;

namespace TestRestFulAPI.Services
{
    public interface IUrunService
    {
        void UrunEkle(Urun urun);

        bool UrunMiktarGuncelle(Urun _urun, int _degisenMiktar);

        void UrunKontrolu(string _urunID, out Urun _urun);


        decimal UrunMiktarKontrolu(string _urunID, out Urun urun);


    }
}
