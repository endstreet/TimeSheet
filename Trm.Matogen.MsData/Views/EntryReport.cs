namespace Trm.MaLogger.MsData.Views
{
    public class EntryReport
    {

        public string User { get; set; } = null!;
        public string Project { get; set; } = null!;
        public string Client { get; set; } = null!;
        public string? Description { get; set; } = null!;
        private DateTimeOffset _startTime;
        public DateTimeOffset StartTime
        {
            get
            {
                return _startTime.ToLocalTime();
            }
            set
            {
                _startTime = value;
            }
        }
        private DateTimeOffset _endTime;
        public DateTimeOffset EndTime
        {
            get { return _endTime.ToLocalTime(); }
            set { _endTime = value; }
        }

        public double Hours
        {
            get
            {
                if (StartTime == DateTimeOffset.MinValue) return 0;
                if (EndTime > StartTime) return (EndTime - StartTime).TotalHours;
                return (DateTime.Now - StartTime).TotalHours;
            }
        }
        public string EntryTime
        {
            get
            {
                return $"{EntrySpan.Days.ToString().PadLeft(2, '0')} : {EntrySpan.Hours.ToString().PadLeft(2, '0')} : {EntrySpan.Minutes.ToString().PadLeft(2, '0')}";
            }
        }

        //Todo: do not need conversion to localtime in this object as we are just calculating the duration.
        //private TimeSpan Span
        //{
        //    get
        //    {
        //        if (LocalStartTime == DateTime.MinValue.ToUniversalTime()) return new TimeSpan(0);
        //        if (LocalEndTime > LocalStartTime) return LocalEndTime - LocalStartTime;
        //        return DateTime.Now - LocalStartTime;
        //    }
        //}

        private TimeSpan EntrySpan
        {
            get
            {
                if (StartTime == DateTimeOffset.MinValue) return new TimeSpan(0);
                if (EndTime > StartTime) return EndTime - StartTime;
                return DateTime.Now - StartTime;
            }
        }

        //private DateTime LocalStartTime
        //{
        //    get
        //    {
        //        if (StartTime == DateTime.MinValue.ToUniversalTime())
        //        {
        //            return DateTime.MinValue;
        //        }
        //        if (StartTime.Kind == DateTimeKind.Local)
        //        {
        //            return StartTime;
        //        }
        //        else
        //        {
        //            return TimeZoneInfo.ConvertTimeFromUtc(StartTime, TimeZoneInfo.Local);
        //        }
        //    }
        //}
        //private DateTime LocalEndTime
        //{
        //    get
        //    {
        //        if (EndTime == DateTime.MinValue.ToUniversalTime())
        //        {
        //            return DateTime.MinValue;
        //        }
        //        if (EndTime.Kind == DateTimeKind.Local)
        //        {
        //            return EndTime;
        //        }
        //        else
        //        {
        //            return TimeZoneInfo.ConvertTimeFromUtc(EndTime, TimeZoneInfo.Local);
        //        }
        //    }
        //}
    }
}
