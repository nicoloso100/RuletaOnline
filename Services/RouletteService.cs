

using RuletaOnline.Infrastructure.Repositories;

namespace RuletaOnline.Services
{
    public class RouletteService : IRouletteService
    {
        private readonly IRouletteRepository rouletteRepository;
        public RouletteService(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
        }
        public int CreateRoulette()
        {
            var rouletteId = rouletteRepository.CreateNewRoulette();
            return rouletteId;
        }
    }
}