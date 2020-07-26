using RuletaOnline.Objects;

namespace RuletaOnline.Infrastructure.Repositories
{
    public interface IRouletteRepository
    {
        long GetNextId();
        void CreateNewRoulette(Roulette newRoulette);
        void ChangeRouletteState(long rouletteId);
    }
}