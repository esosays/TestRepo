using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestFulAPI.Entities
{
    public interface ISepetRepo
    {
        ICollection<Sepet> SepetGetir();
        bool SepetGetir(int _musteriID, out Sepet _sepet);
        //bool SepetUrunKontrolu(string _urunID, int _musteriID, out Urun _urun);
        Sepet SepetGuncelle(int musteriID,Sepet _sepet);
        void SepetSil(int _musteriID);
    }
}
