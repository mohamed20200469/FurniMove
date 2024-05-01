using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FurniMove.Models
{
    public class AppUser : IdentityUser
    {
        public string? Role { get; set; }
        public int MoveCounter { get; set; } = 0;
        public string? UserImgURL { get; set; }
        [NotMapped]
        public IFormFile? ImgFile { get; set; }
        public bool Suspended { get; set; } = false;
    }
}
