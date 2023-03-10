

using System.ComponentModel.DataAnnotations;

namespace Trm.MaLogger.MsData.Models
{
    public class UserProject
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public bool IsProjectManager { get; set; }
    }
}
