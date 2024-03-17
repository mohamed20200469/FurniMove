using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace FurniMove.Models
{
    public class AppUser : IdentityUser
    {
        public string? Role { get; set; }
        public int MoveCounter { get; set; } = 0;
    }
}
