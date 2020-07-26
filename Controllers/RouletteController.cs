using Microsoft.AspNetCore.Mvc;
using RuletaOnline.Services;

namespace RuletaOnline.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteService rouletteService;
        public RouletteController(IRouletteService rouletteService)
        {
            this.rouletteService = rouletteService;
        }

        [HttpPost]
        public int CreateRoulette()
        {
            var rouletteId = rouletteService.CreateRoulette();
            return rouletteId;
        }

        [HttpGet]
        public string Prueba()
        {
            return "Ok";
        }
    }
}