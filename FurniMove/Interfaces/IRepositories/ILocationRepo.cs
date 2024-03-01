using FurniMove.Models;

namespace FurniMove.Interfaces.IRepositories
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
