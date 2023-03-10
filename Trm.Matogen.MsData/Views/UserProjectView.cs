namespace Trm.MaLogger.MsData.Views
{
    public class UserProjectView
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string ClientName { get; set; } = null!;
        public string ProjectName { get; set; } = null!;
        public bool IsProjectManager { get; set; }
        public bool IsProjectMember { get; set; }
    }
}
