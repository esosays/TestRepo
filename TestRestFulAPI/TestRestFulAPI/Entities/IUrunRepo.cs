using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestFulAPI.Entities
{
    public interface IUrunRepo
    {
        ICollection<Urun> UrunGetir();
        bool UrunKontrolu(string _urunID, out Urun _urun);
        void UrunEkle(Urun urun);

        bool UrunMiktarGuncelle(Urun _urun, int _degisenMiktar);
    }
}
