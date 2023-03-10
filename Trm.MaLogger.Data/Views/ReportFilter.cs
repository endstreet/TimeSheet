namespace Trm.MaLogger.Data.Views
{
    public class ReportFilter
    {
        public ReportFilter(string name, string type, object value)
        {
            Name = name;
            ValueType = type;
            Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
        /// <summary>
        /// To select the control type on the UI
        /// </summary>
        public string ValueType { get; set; }
    }
}
