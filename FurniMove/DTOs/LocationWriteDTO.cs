using System.ComponentModel.DataAnnotations;

namespace FurniMove.DTOs
{
    public class LocationWriteDTO
    {
        [Required]
        public double latitude { get; set; }
        [Required]
        public double longitude { get; set; }
    }
}
