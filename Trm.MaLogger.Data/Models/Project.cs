using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Trm.MaLogger.Data.Models
{
    public class Project
    {

        public Project()
        {
            CreatedOn = DateTime.Now;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Color { get; set; } = "#000";
        public string ClientId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

    }
}
