using MongoDB.Bson.Serialization.Attributes;
using RuletaOnline.Objects.Enums;

namespace RuletaOnline.DTOs
{
    [BsonIgnoreExtraElements]
    public class DTOBet
    {
        public long RouletteId { get; set; }
        public int BetAmount { get; set; }
        public int? BetNumber { get; set; }
        public string BetColor { get; set; }
    }
}