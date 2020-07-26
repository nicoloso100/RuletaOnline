using System.Threading.Tasks;
using RuletaOnline.Objects;

namespace RuletaOnline.Services
{
    public interface IRouletteService
    {
        long CreateRoulette();
        void EnableRoulette(long rouletteId);
        Task BetOnRoulette(Bet bet);
    }
}