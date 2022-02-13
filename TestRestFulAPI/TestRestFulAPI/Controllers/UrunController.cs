using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestRestFulAPI.Entities;
using TestRestFulAPI.Services;

namespace TestRestFulAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UrunController : Controller
    {


        private readonly IUrunRepo Gnl_UrunRepo;

        private readonly ISepetRepo Gnl_sepetRepo;
        private readonly ISepetService Gnl_sepetServices;
        private readonly ILogger<UrunController> Gnl_logger;



        public UrunController(IUrunRepo urunRepo, ISepetRepo sepetRepo, ISepetService sepetService, ILogger<UrunController> logger)
        {
            Gnl_UrunRepo = urunRepo;
            Gnl_sepetRepo = sepetRepo;
            Gnl_sepetServices = sepetService;
            Gnl_logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<Urun>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<List<Urun>> Get()
        {
            var ret_urun = Gnl_UrunRepo.UrunGetir();

            if (ret_urun == null)
            {
                return UrunBulunamadi();
            }
            return ret_urun.ToList();
        }

      

        private ActionResult UrunBulunamadi()
        {
            var message = $"Tanımlı ürün bulunamadı.";
            Gnl_logger.LogWarning(message);
            return NotFound(message);
        }

    }
}
