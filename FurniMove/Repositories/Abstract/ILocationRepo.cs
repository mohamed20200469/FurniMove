using FurniMove.Models;

namespace FurniMove.Repositories.Abstract
{
    public interface ILocationRepo
    {
        public Task<Location?> GetLocationById(int id);
        public Task<ICollection<Location>> GetAllLocations();
        public Task<bool> UpdateLocation(Location location);
        public Task<bool> CreateLocation(Location location);
        public Task<bool> DeleteLocationById(int id);
        public Task<bool> Save();
    }
}
