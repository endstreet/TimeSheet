using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Trm.MaLogger.Data.Models
{
    public class TimeEntry
    {

        public TimeEntry()
        {
            CreatedOn = DateTime.Now;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Description { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string ProjectId { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Hours { get; set; }
        public bool Running { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

    }
}
