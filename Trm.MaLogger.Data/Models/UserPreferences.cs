using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Trm.MaLogger.Data.Models
{
    internal class UserPreferences
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? ProjectId { get; set; }

    }
}
