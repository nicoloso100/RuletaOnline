using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RuletaOnline.Infrastructure.Models
{
    public class RouletteModel
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public int State { get; set; }
    }
}