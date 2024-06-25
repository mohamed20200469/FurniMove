using System.ComponentModel.DataAnnotations;

namespace FurniMove.DTOs
{
    public class ApplianceWriteDTO
    {
        [Required]
        public string description { get; set; } = "";
        [Required]
        public int? moveRequestId { get; set; }
        [Required]
        public IFormFile? Img { get; set; }
    }
}
