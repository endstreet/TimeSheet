namespace Trm.MaLogger.MsData.Models
{
    public class TimeEntry
    {

        public TimeEntry()
        {
            CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }
        public string? Description { get; set; } = null!;
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public decimal Hours { get; set; }
        public bool Running { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; } = DateTime.Now;

    }
}
