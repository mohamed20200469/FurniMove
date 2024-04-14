using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}
