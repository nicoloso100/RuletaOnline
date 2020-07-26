using MongoDB.Driver;
using RuletaOnline.Infrastructure.Models;

namespace RuletaOnline.Infrastructure
{
    public interface IRouletteContext
    {
        IMongoCollection<RouletteModel> Roulettes { get; }
    }
}