using System.ComponentModel.DataAnnotations;

namespace FurniMove.DTOs
{
    public class TruckWriteDTO
    {
        [Required]
        public string? PlateNumber { get; set; }
        [Required]
        public string? Brand { get; set; }
        [Required]
        public string? Model { get; set; }
        [Required]
        public int? Year { get; set; }
        [Required]
        public string? Type { get; set; }
    }
}
