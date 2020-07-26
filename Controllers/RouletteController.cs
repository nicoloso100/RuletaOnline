using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RuletaOnline.DTOs;
using RuletaOnline.Objects;
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
        public long CreateRoulette()
        {
            var rouletteId = rouletteService.CreateRoulette();
            return rouletteId;
        }

        [HttpPost]
        public IActionResult EnableRoulette([FromBody] long rouletteId)
        {
            rouletteService.EnableRoulette(rouletteId);
            return Ok("La ruleta se ha habilitado correctamente");
        }

        [HttpPost]
        public async Task<IActionResult> BetOnRoulette([FromBody] DTOBet bet)
        {
            var headerUserParameter = Request.Headers["user"].ToString();
            var newBet = new Bet(
                rouletteId: bet.RouletteId,
                user: headerUserParameter,
                amount: bet.BetAmount,
                betNumber: bet.BetNumber,
                betColor: bet.BetColor.ToString()
            );
            await rouletteService.BetOnRoulette(newBet);
            return Ok("La apuesta se ha realizado correctamente");
        }

        [HttpPost]
        public List<DTOBet> DisableRulette([FromBody] long rouletteId)
        {
            var disableAndResponseTask = rouletteService.DisableRoulette(rouletteId);
            disableAndResponseTask.Wait();
            return disableAndResponseTask.Result;
        }

        [HttpGet]
        public async Task<List<DTORoulette>> GetAllRoulettes()
        {
            return await rouletteService.GetAllRoulettes();
        }
    }
}