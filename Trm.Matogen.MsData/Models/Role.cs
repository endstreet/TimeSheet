
using System.ComponentModel.DataAnnotations;

namespace Trm.MaLogger.MsData.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
