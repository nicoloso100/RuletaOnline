namespace RuletaOnline.Infrastructure.Repositories
{
    public interface IRouletteRepository
    {
        int CreateNewRoulette();
        void ChangeRouletteState(int rouletteId);
    }
}