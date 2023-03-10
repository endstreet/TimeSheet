namespace Trm.MaLogger.MsData.Views
{
    public class DialogModel
    {
        public DialogModel()
        {
            Title = "Confirm";
            Message = "Are you sure ?";
            Show = false;
        }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Show { get; set; }

    }
}
