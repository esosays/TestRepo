using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestFulAPI.Entities
{
    public interface ISiparisRepo
    {
        List<Siparis> SiparisGetirHepsi();
        List<Siparis> SiparisGetir(int _musteriID);

        Siparis SiparisEkle(int musteriID, out Siparis _siparis, out string RetAck);
        //bool SiparisUrunKontrolu(string _urunID, int _musteriID, out Siparis _siparis);
    }
}
