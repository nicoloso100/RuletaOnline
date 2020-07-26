using MongoDB.Bson;
using RuletaOnline.Infrastructure.Models;
using RuletaOnline.Objects;

namespace RuletaOnline.Infrastructure.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly IRouletteContext rouletteContext;
        public RouletteRepository(IRouletteContext rouletteContext)
        {
            this.rouletteContext = rouletteContext;
        }

        public long GetNextId()
        {
            var nextId = rouletteContext.Roulettes.CountDocuments(new BsonDocument()) + 1;
            return nextId;
        }
        public void CreateNewRoulette(Roulette newRoulette)
        {
            var roulette = new RouletteModel
            {
                Id = newRoulette.GetId(),
                State = (int)newRoulette.GetState()
            };
            rouletteContext.Roulettes.InsertOne(roulette);
        }

        public void ChangeRouletteState(long rouletteId)
        {
            throw new System.NotImplementedException();
        }
    }
}