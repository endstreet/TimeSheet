
using System.ComponentModel.DataAnnotations;

namespace Trm.MaLogger.MsData.Models
{

    public class Client
    {
        public Client()
        {
            CreatedOn = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
