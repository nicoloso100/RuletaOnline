using MongoDB.Driver;
using RuletaOnline.Configuration;
using RuletaOnline.Infrastructure.Models;

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

        public IMongoCollection<RouletteModel> Roulettes => _db.GetCollection<RouletteModel>("Roulette");
    }
}