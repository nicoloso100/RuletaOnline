using System.Collections.Generic;
using System.Threading.Tasks;
using RuletaOnline.DTOs;
using RuletaOnline.Objects;

namespace RuletaOnline.Services
{
    public interface IRouletteService
    {
        long CreateRoulette();
        void EnableRoulette(long rouletteId);
        Task BetOnRoulette(Bet bet);
        Task<List<DTOBet>> DisableRoulette(long rouletteId);
        Task<List<DTORoulette>> GetAllRoulettes();
    }
}