using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Trm.MaLogger.Data.Models
{
    public class UserRole
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string RoleId { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
