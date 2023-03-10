using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Trm.MaLogger.Data.Models
{
    public class User
    {
        public User()
        {
            CreatedOn = DateTime.Now;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? OTP { get; set; }
        public byte[] Salt { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

    }
}
