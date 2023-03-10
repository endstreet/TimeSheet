using Trm.MaLogger.Data.Models;

namespace Trm.MaLogger.Data.Views
{
    public class ActiveUser
    {
        public ActiveUser()
        {
            Name = "";
            Roles = new List<Role>();
        }
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Otp { get; set; }
        public DateTime StartTime { get; set; }
        public List<Role> Roles = null!;
        public List<Project> Projects = null!;
        public bool IsInRole(IList<string> componentRoles)
        {
            return userRoles.Intersect(componentRoles).Any();
        }
        private List<string> userRoles //role names
        {
            get
            {
                return Roles.Where(r => r.Name != null).Select(r => r.Name).ToList();
            }
        }

    }
}
