using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RuletaOnline.DTOs;
using RuletaOnline.ExceptionMiddlewares;
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
            rouletteService.EnableRoulette(rouletteId: rouletteId);

            return Ok("La ruleta se ha habilitado correctamente");
        }

        [HttpPost]
        public async Task<IActionResult> BetOnRoulette([FromBody] DTOBet bet)
        {
            var headerUserParameter = Request.Headers["user"].ToString();
            await rouletteService.BetOnRoulette(bet: bet, user: headerUserParameter);

            return Ok("La apuesta se ha realizado correctamente");
        }

        [HttpPost]
        public async Task<List<DTOBet>> DisableRulette([FromBody] long rouletteId)
        {
            var disableAndResponseTask = rouletteService.DisableRoulette(rouletteId: rouletteId);

            return await disableAndResponseTask;
        }

        [HttpGet]
        public async Task<List<DTORoulette>> GetAllRoulettes()
        {
            return await rouletteService.GetAllRoulettes();
        }
    }
}