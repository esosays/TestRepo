using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;

namespace TestRestFulAPI.Services
{
    public interface ISiparisService
    {
        void SiparisEkle(int musteriID,out Siparis siparis, out string outAck);
    }
}
