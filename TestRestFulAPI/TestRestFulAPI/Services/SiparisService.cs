using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;

namespace TestRestFulAPI.Services
{
    public class SiparisService : ISiparisService
    {

        private readonly ISiparisRepo Gnl_siparisRepo;
        private readonly ILogger<SiparisService> Gnl_logger;

        public SiparisService(ISiparisRepo _siparisRepo, ILogger<SiparisService> _logger)
        {
            Gnl_siparisRepo = _siparisRepo;
            Gnl_logger = _logger;
        }
        public void SiparisEkle(int musteriID, out Siparis siparis,out string outAck)
        {            
            Gnl_siparisRepo.SiparisEkle(musteriID, out siparis, out outAck);
        }
    }
}
