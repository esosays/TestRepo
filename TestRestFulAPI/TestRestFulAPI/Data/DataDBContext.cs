using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;

namespace TestRestFulAPI.Data
{
    public class DataDBContext : IDataDBContext
    {
        public DataDBContext()
        {
            DBSiparis = new Dictionary<Guid, Siparis>();
            DBSepet = new Dictionary<int, Sepet>();
            DBUrun = new List<Urun>();
            DBUrun.Add(new Urun("P1", "Urun1", 55, 4));
            DBUrun.Add(new Urun("P2", "Urun2", 150, 3));
            DBUrun.Add(new Urun("P3", "Urun3", 45, 1));
            DBUrun.Add(new Urun("P4", "Urun4", 10, 1));
            DBUrun.Add(new Urun("P5", "Urun5", 65, 2));


        }

        public Dictionary<Guid, Siparis> DBSiparis { get; set; }


        public Dictionary<int, Sepet> DBSepet { get; set; }
        

        public List<Urun> DBUrun { get; set; }
    }

    public interface IDataDBContext
    {
        Dictionary<int, Sepet> DBSepet { get; set; }
        Dictionary<Guid, Siparis> DBSiparis { get; set; }

        List<Urun> DBUrun { get; set; }

    }

}
