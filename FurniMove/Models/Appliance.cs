using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurniMove.Models
{
    public class Appliance
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string description { get; set; } = "";
        public MoveRequest? moveRequest { get; set; }
        [ForeignKey("moveRequest")]
        public int? moveRequestId { get; set; }
        public string? ImgURL { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
