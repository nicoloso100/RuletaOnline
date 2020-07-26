using System.Threading.Tasks;
using RuletaOnline.DTOs;
using RuletaOnline.Objects;

namespace RuletaOnline.Infrastructure.Repositories
{
    public interface IRouletteRepository
    {
        long GetNextRouletteId();
        Task<DTORoulette> GetRouletteById(long rouletteId);
        void CreateNewRoulette(Roulette newRoulette);
        void ModifyRoulette(Roulette newRoulette);
        Task CreateBetOnRoulette(Bet newBet);
        RouletteStates GetRouletteStateById(long rouletteId);
    }
}