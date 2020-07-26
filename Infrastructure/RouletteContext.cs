using MongoDB.Driver;
using RuletaOnline.Configuration;
using RuletaOnline.Infrastructure.Documents;

namespace RuletaOnline.Infrastructure
{
    public class RouletteContext : IRouletteContext
    {
        private readonly IMongoDatabase _db;
        public RouletteContext(IServerConfiguration ServerConfiguration)
        {
            var config = ServerConfiguration.GetMongoDBConfig();
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<RouletteDocument> Roulettes => _db.GetCollection<RouletteDocument>("Roulette");
        public IMongoCollection<BetDocument> Bets => _db.GetCollection<BetDocument>("Bets");
    }
}