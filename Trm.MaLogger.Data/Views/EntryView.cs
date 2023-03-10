using Trm.MaLogger.Data.Models;

namespace Trm.MaLogger.Data.Views
{
    public class EntryView
    {

        public EntryView(string userId)
        {
            ActiveProject = new Project();
            StartTime = DateTime.MinValue.ToUniversalTime();
            EndTime = DateTime.MinValue.ToUniversalTime();
            UserId = userId;
        }
        public EntryView()
        {
            ActiveProject = new Project();
            StartTime = DateTime.MinValue.ToUniversalTime();
            EndTime = DateTime.MinValue.ToUniversalTime();
        }
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string ProjectId { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Project ActiveProject { get; set; } = null!;
        public string Client { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Hours
        {
            get
            {
                if (LocalStartTime == DateTime.MinValue.ToUniversalTime()) return new TimeSpan(0);
                if (LocalEndTime > LocalStartTime) return LocalEndTime - LocalStartTime;
                return DateTime.Now - LocalStartTime;
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

        public DateTime LocalStartTime
        {
            get
            {
                if (StartTime == DateTime.MinValue.ToUniversalTime())
                {
                    return DateTime.MinValue;
                }
                if (StartTime.Kind == DateTimeKind.Local)
                {
                    return StartTime;
                }
                else
                {
                    return TimeZoneInfo.ConvertTimeFromUtc(StartTime, TimeZoneInfo.Local);
                }
            }
        }
        public DateTime LocalEndTime
        {
            get
            {
                if (EndTime == DateTime.MinValue.ToUniversalTime())
                {
                    return DateTime.MinValue;
                }
                if (EndTime.Kind == DateTimeKind.Local)
                {
                    return EndTime;
                }
                else
                {
                    return TimeZoneInfo.ConvertTimeFromUtc(EndTime, TimeZoneInfo.Local);
                }
            }
        }
    }
}
