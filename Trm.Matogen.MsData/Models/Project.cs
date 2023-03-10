using System.ComponentModel.DataAnnotations;

namespace Trm.MaLogger.MsData.Models
{
    public class Project
    {

        public Project()
        {
            CreatedOn = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Color { get; set; } = "#000";
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

    }
}
