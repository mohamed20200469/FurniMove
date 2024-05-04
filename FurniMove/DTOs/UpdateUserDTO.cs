using System.ComponentModel.DataAnnotations;

namespace FurniMove.DTOs
{
    public class UpdateUserDTO
    {
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? OldPassword { get; set; }
        public string? Password { get; set; } = null;
    }
}
