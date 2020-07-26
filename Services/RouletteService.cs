

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
            var id = rouletteRepository.GetNextId();
            var newRoulette = new Roulette(id, RouletteStates.inactive);
            rouletteRepository.CreateNewRoulette(newRoulette: newRoulette);
            return newRoulette.GetId();
        }
    }
}