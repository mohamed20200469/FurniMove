using System.ComponentModel.DataAnnotations;

namespace FurniMove.DTOs
{
    public class TruckWriteDTO
    {
        [Required]
        public string? plateNumber { get; set; }
        [Required]
        public string? brand { get; set; }
        [Required]
        public string? model { get; set; }
        [Required]
        public int? year { get; set; }
        [Required]
        public int capacity { get; set; }
        [Required]
        public double ConsumptionRate { get; set; }
    }
}
