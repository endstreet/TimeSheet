using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Trm.MaLogger.Data.Models
{
    public class Report
    {
        public Report(string user, string report, string fileType, byte[] content)
        {
            User = user;
            ReportName = report;
            ReportDate = DateTimeOffset.Now;
            Content = content;
            FileType = fileType;

        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string User { get; set; }
        public string ReportName { get; set; }
        public string FileType { get; set; }
        public DateTimeOffset ReportDate { get; set; }
        public byte[] Content { get; set; }
    }
}
