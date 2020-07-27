

using System.Collections.Generic;
using System.Threading.Tasks;
using RuletaOnline.DTOs;
using RuletaOnline.ExceptionMiddlewares;
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
            var newRoulette = new Roulette(id: id, state: RouletteStates.inactive);
            rouletteRepository.CreateNewRoulette(newRoulette: newRoulette);

            return newRoulette.GetId();
        }

        public void EnableRoulette(long rouletteId)
        {
            var roulette = CheckIfRouletteExists(rouletteId: rouletteId);
            roulette.Wait();
            if (roulette.Result.State == RouletteStates.active)
                throw new HttpResponseException("La ruleta ingresada ya se encuentra activa.");
            var newRoulette = new Roulette(id: roulette.Result.RouletteId, state: RouletteStates.active);
            rouletteRepository.ModifyRoulette(newRoulette: newRoulette);
        }

        public Task BetOnRoulette(DTOBet bet, string user)
        {
            var newBet = new Bet(
                rouletteId: bet.RouletteId,
                user: user,
                amount: bet.BetAmount,
                betNumber: bet.BetNumber,
                betColor: bet.BetColor.ToString()
            );
            var state = rouletteRepository.GetRouletteStateById(rouletteId: newBet.GetRouletteId());
            if (state == RouletteStates.inactive)
                throw new HttpResponseException("La ruleta ingresada no se encuentra activa.");

            return rouletteRepository.CreateBetOnRoulette(newBet: newBet);
        }

        public async Task<List<DTOBet>> DisableRoulette(long rouletteId)
        {
            var roulette = await CheckIfRouletteExists(rouletteId: rouletteId);
            if (roulette.State == RouletteStates.inactive)
                throw new HttpResponseException("La ruleta ingresada ya se encuentra inactiva.");
            var newRoulette = new Roulette(id: roulette.RouletteId, state: RouletteStates.inactive);
            rouletteRepository.ModifyRoulette(newRoulette: newRoulette);
            var bets = await GetRouletteSummary(rouletteId: rouletteId);

            return bets;
        }

        private Task<List<DTOBet>> GetRouletteSummary(long rouletteId)
        {
            var getBetsTask = rouletteRepository.GetBetsByRouletteId(rouletteId: rouletteId);

            return getBetsTask;
        }

        private async Task<DTORoulette> CheckIfRouletteExists(long rouletteId)
        {
            var roulette = await rouletteRepository.GetRouletteById(rouletteId: rouletteId);
            if (roulette is null)
                throw new HttpResponseException("No se ha encontrado la ruleta ingresada.");

            return roulette;
        }

        public async Task<List<DTORoulette>> GetAllRoulettes()
        {
            return await rouletteRepository.GetAllRoulettes();
        }
    }
}