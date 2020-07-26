

using System.Threading.Tasks;
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
            var roulette = rouletteRepository.GetRouletteById(rouletteId);
            roulette.Wait();
            if (roulette is null)
                throw new System.Exception("No se ha encontrado la ruleta ingresada.");
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
    }
}