using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RuletaOnline.Objects;

namespace RuletaOnline.Infrastructure.Documents
{
    public class RouletteDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalID { get; set; }
        public long RouletteId { get; set; }
        public int State { get; set; }
    }
}