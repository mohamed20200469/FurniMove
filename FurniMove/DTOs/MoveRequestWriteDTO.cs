using System.ComponentModel.DataAnnotations;

namespace FurniMove.DTOs
{
    public class MoveRequestWriteDTO
    {
        [Required]
        public int startLocationId { get; set; }
        [Required] 
        public int endLocationId { get; set; }
        [Required]
        public int numOfAppliances { get; set; }
        [Required]
        public DateOnly StartDate {  get; set; }
        [Required]
        public string? VehicleType { get; set; }
    }
}
