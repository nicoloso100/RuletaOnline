using System.Collections.Generic;
using System.Threading.Tasks;
using RuletaOnline.DTOs;
using RuletaOnline.Objects;

namespace RuletaOnline.Infrastructure.Repositories
{
    public interface IRouletteRepository
    {
        long GetNextRouletteId();
        Task<DTORoulette> GetRouletteById(long rouletteId);
        void CreateNewRoulette(IRoulette newRoulette);
        void ModifyRoulette(IRoulette newRoulette);
        Task CreateBetOnRoulette(IBet newBet);
        RouletteStates GetRouletteStateById(long rouletteId);
        Task<List<DTOBet>> GetBetsByRouletteId(long rouletteId);
        Task<List<DTORoulette>> GetAllRoulettes();
    }
}