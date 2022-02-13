using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestFulAPI.Entities
{
    public class Siparis
    {

        public int MusteriID { get; set; }
        public Guid SiparisID { get; set; }

        public DateTime SiparisTarihi { get; set; }

        public List<Urun> SatilanUrunler { get; set; }

    }
}
