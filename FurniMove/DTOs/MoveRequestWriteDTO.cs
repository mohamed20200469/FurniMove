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
    }
}
