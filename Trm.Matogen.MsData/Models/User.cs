using System.ComponentModel.DataAnnotations;

namespace Trm.MaLogger.MsData.Models
{
    public class User
    {
        public User()
        {
            CreatedOn = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? OTP { get; set; }
        public byte[] Salt { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

    }
}
