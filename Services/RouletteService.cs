

using System.Collections.Generic;
using System.Threading.Tasks;
using RuletaOnline.DTOs;
using RuletaOnline.Infrastructure.Repositories;
using RuletaOnline.Objects;

namespace RuletaOnline.Services
{
    public class RouletteService : IRouletteService
    {
        private readonly IRouletteRepository rouletteRepository;
        public RouletteService(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
        }

        public long CreateRoulette()
        {
            var id = rouletteRepository.GetNextRouletteId();
            var newRoulette = new Roulette(id, RouletteStates.inactive);
            rouletteRepository.CreateNewRoulette(newRoulette: newRoulette);
            return newRoulette.GetId();
        }

        public void EnableRoulette(long rouletteId)
        {
            var roulette = CheckIfRouletteExists(rouletteId);
            roulette.Wait();
            if (roulette.Result.State == RouletteStates.active)
                throw new System.Exception("La ruleta ingresada ya se encuentra activa.");
            var newRoulette = new Roulette(roulette.Result.RouletteId, RouletteStates.active);
            rouletteRepository.ModifyRoulette(newRoulette);
        }

        public Task BetOnRoulette(Bet bet)
        {
            var state = rouletteRepository.GetRouletteStateById(bet.GetRouletteId());
            if (state == RouletteStates.inactive)
                throw new System.Exception("La ruleta ingresada no se encuentra activa.");
            return rouletteRepository.CreateBetOnRoulette(bet);
        }

        public async Task<List<DTOBet>> DisableRoulette(long rouletteId)
        {
            var roulette = await CheckIfRouletteExists(rouletteId);
            if (roulette.State == RouletteStates.inactive)
                throw new System.Exception("La ruleta ingresada ya se encuentra inactiva.");
            var newRoulette = new Roulette(roulette.RouletteId, RouletteStates.inactive);
            rouletteRepository.ModifyRoulette(newRoulette);
            var bets = await GetRouletteSummary(rouletteId);
            return bets;
        }

        private Task<List<DTOBet>> GetRouletteSummary(long rouletteId)
        {
            var getBetsTask = rouletteRepository.GetBetsByRouletteId(rouletteId);
            return getBetsTask;
        }

        private async Task<DTORoulette> CheckIfRouletteExists(long rouletteId)
        {
            var roulette = await rouletteRepository.GetRouletteById(rouletteId);
            if (roulette is null)
                throw new System.Exception("No se ha encontrado la ruleta ingresada.");
            return roulette;
        }

        public async Task<List<DTORoulette>> GetAllRoulettes()
        {
            return await rouletteRepository.GetAllRoulettes();
        }
    }
}