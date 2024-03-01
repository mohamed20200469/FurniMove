using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurniMove.Models
{
    public class Appliance
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double length { get; set; }
        [Required]
        public string description { get; set; } = "";
        [Required]
        public double width { get; set; }
        [Required]
        public double height { get; set; }
        public MoveRequest? moveRequest { get; set; }
        [ForeignKey("moveRequest")]
        public int? moveRequestId { get; set; }
        //image should be here
    }
}
