using MongoDB.Bson.Serialization.Attributes;
using RuletaOnline.Objects;

namespace RuletaOnline.DTOs
{
    [BsonIgnoreExtraElements]
    public class DTORoulette
    {
        public long RouletteId { get; set; }
        public RouletteStates State { get; set; }
    }
}