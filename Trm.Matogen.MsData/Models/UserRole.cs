

using System.ComponentModel.DataAnnotations;

namespace Trm.MaLogger.MsData.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
