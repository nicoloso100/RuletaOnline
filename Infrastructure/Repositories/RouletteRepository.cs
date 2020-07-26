using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using RuletaOnline.DTOs;
using RuletaOnline.Infrastructure.Documents;
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
        public long GetNextRouletteId()
        {
            var nextId = rouletteContext.Roulettes.CountDocuments(new BsonDocument()) + 1;
            return nextId;
        }
        public async Task<DTORoulette> GetRouletteById(long rouletteId)
        {
            var roulette = await rouletteContext.Roulettes.Find<RouletteDocument>(x => x.RouletteId == rouletteId).As<DTORoulette>().FirstOrDefaultAsync();
            return roulette;
        }
        public void CreateNewRoulette(Roulette newRoulette)
        {
            var roulette = new RouletteDocument
            {
                RouletteId = newRoulette.GetId(),
                State = (int)newRoulette.GetState()
            };
            rouletteContext.Roulettes.InsertOne(roulette);
        }

        public void ModifyRoulette(Roulette newRoulette)
        {
            var filter = Builders<RouletteDocument>.Filter.Eq("RouletteId", newRoulette.GetId());
            var update = Builders<RouletteDocument>.Update.Set("State", (int)newRoulette.GetState());
            rouletteContext.Roulettes.UpdateOne(filter, update);
        }

        public async Task CreateBetOnRoulette(Bet newBet)
        {
            var bet = new BetDocument
            {
                RouletteId = newBet.GetRouletteId(),
                BetUser = newBet.GetUser(),
                BetAmount = newBet.GetAmount(),
                BetColor = newBet.GetBetColor().ToString(),
                BetNumber = newBet.GetBetNumber()
            };
            await rouletteContext.Bets.InsertOneAsync(bet);
        }

        public RouletteStates GetRouletteStateById(long rouletteId)
        {
            var roulette = rouletteContext.Roulettes.Find<RouletteDocument>(x => x.RouletteId == rouletteId).As<DTORoulette>().FirstOrDefault();
            return roulette.State;
        }

        public async Task<List<DTOBet>> GetBetsByRouletteId(long rouletteId)
        {
            var bets = await rouletteContext.Bets.Find(x => true).As<DTOBet>().ToListAsync();
            return bets;
        }

        public async Task<List<DTORoulette>> GetAllRoulettes()
        {
            var roulettes = await rouletteContext.Roulettes.Find(x => true).As<DTORoulette>().ToListAsync();
            return roulettes;
        }
    }
}