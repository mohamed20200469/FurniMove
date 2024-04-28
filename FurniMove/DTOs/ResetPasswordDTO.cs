using System.ComponentModel.DataAnnotations;

namespace FurniMove.DTOs
{
    public class ResetPasswordDTO
    {
        [Required]
        public string? currentPassword { get; set; }
        [Required]
        public string? newPassword { get; set; }
    }
}
