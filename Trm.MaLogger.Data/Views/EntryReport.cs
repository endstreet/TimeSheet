namespace Trm.MaLogger.Data.Views
{
    public class EntryReport
    {

        public string User { get; set; } = null!;
        public string Project { get; set; } = null!;
        public string Client { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Hours
        {
            get
            {
                if (LocalStartTime == DateTime.MinValue.ToUniversalTime()) return 0;
                if (LocalEndTime > LocalStartTime) return (LocalEndTime - LocalStartTime).TotalHours;
                return (DateTime.Now - LocalStartTime).TotalHours;
            }
        }

        public string EntryTime
        {
            get
            {
                return $"{Span.Days.ToString().PadLeft(2, '0')} : {Span.Hours.ToString().PadLeft(2, '0')} : {Span.Minutes.ToString().PadLeft(2, '0')}";
            }
        }

        //Todo: do not need conversion to localtime in this object as we are just calculating the duration.
        private TimeSpan Span
        {
            get
            {
                if (LocalStartTime == DateTime.MinValue.ToUniversalTime()) return new TimeSpan(0);
                if (LocalEndTime > LocalStartTime) return LocalEndTime - LocalStartTime;
                return DateTime.Now - LocalStartTime;
            }
        }

        private DateTime LocalStartTime
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
        private DateTime LocalEndTime
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
