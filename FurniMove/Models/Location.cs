using System.ComponentModel.DataAnnotations;

namespace FurniMove.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double longitude { get; set; }
        [Required]
        public double latitude { get; set; }
        [Required]
        public DateTime timeStamp { get; set; }
    }
}
