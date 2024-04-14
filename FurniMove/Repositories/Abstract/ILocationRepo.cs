using FurniMove.Models;

namespace FurniMove.Repositories.Abstract
{
    public interface ILocationRepo
    {
        public Location? GetLocationById(int id);
        public ICollection<Location> GetAllLocations();
        public bool UpdateLocation(Location location);
        public bool CreateLocation(Location location);
        public bool DeleteLocationById(int id);
        bool Save();
    }
}
