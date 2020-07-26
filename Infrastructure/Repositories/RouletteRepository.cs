namespace RuletaOnline.Infrastructure.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly IRouletteContext rouletteContext;
        public RouletteRepository(IRouletteContext rouletteContext)
        {
            this.rouletteContext = rouletteContext;
        }

        public void ChangeRouletteState(int rouletteId)
        {
            throw new System.NotImplementedException();
        }

        public int CreateNewRoulette()
        {
            throw new System.NotImplementedException();
        }
    }
}