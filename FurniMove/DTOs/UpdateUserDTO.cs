using System.ComponentModel.DataAnnotations;

namespace FurniMove.DTOs
{
    public class UpdateUserDTO
    {
        public string? PhoneNumber { get; set; }
        public string? OldPassword { get; set; } = null;
        public string? Password { get; set; } = null;
    }
}
