using MongoDB.Driver;
using RuletaOnline.Infrastructure.Documents;

namespace RuletaOnline.Infrastructure
{
    public interface IRouletteContext
    {
        IMongoCollection<RouletteDocument> Roulettes { get; }
        IMongoCollection<BetDocument> Bets { get; }
    }
}