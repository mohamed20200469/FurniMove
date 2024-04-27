using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface ILocationService
    {
        public Task<bool> DeleteLocationById(int id);
        public Task<bool> CreateLocation(Location location);
        public Task<bool> UpdateLocation(Location location);
        public Task<Location?> GetLocationById(int id);
        public Task<ICollection<Location>> GetAllLocations();
    }
}
