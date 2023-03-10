using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Trm.MaLogger.Data.Models
{
    public class UserProject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string UserId { get; set; } = null!;
        public string ProjectId { get; set; } = null!;
        public bool IsProjectManager { get; set; }
    }
}
