using FurniMove.Models;

namespace FurniMove.Interfaces.IServices
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}
