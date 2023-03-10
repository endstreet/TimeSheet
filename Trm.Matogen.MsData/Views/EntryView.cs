using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.MsData.Views
{
    public class EntryView
    {

        public EntryView(int userId)
        {
            ActiveProject = new Project();
            StartTime = DateTimeOffset.MinValue;
            EndTime = DateTimeOffset.MinValue;
            UserId = userId;
        }
        public EntryView()
        {
            ActiveProject = new Project();
            StartTime = DateTimeOffset.MinValue;
            EndTime = DateTimeOffset.MinValue;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int ClientId { get; set; }
        public string? Description { get; set; } = null!;
        public Project ActiveProject { get; set; } = null!;
        public string Client { get; set; } = null!;
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public TimeSpan Hours
        {
            get
            {
                if (StartTime == DateTimeOffset.MinValue) return new TimeSpan(0);
                if (EndTime > StartTime) return EndTime - StartTime;
                return DateTime.Now - StartTime;
            }
        }
        public string Ticker
        {
            get
            {
                return $"{Hours.Hours.ToString().PadLeft(2, '0')} : {Hours.Minutes.ToString().PadLeft(2, '0')}  : {Hours.Seconds.ToString().PadLeft(2, '0')}";
            }
        }
        public string EntryTime
        {
            get
            {
                return $"{Hours.Hours.ToString().PadLeft(2, '0')} : {Hours.Minutes.ToString().PadLeft(2, '0')}";
            }
        }
        public bool Running { get; set; }


    }
}
