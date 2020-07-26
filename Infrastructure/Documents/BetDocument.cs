using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RuletaOnline.Infrastructure.Documents
{
    public class BetDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalID { get; set; }
        public long RouletteId { get; set; }
        public string BetUser { get; set; }
        public int BetAmount { get; set; }
        public int? BetColor { get; set; }
        public int? BetNumber { get; set; }
    }
}