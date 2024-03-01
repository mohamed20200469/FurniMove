using FurniMove.Data;
using FurniMove.Interfaces.IRepositories;
using FurniMove.Models;

namespace FurniMove.Repositories
{
    public class LocationRepo : ILocationRepo
    {
        private readonly AppDbContext _db;
        public LocationRepo(AppDbContext db)
        {
            _db = db;
        }
        public bool CreateLocation(Location location)
        {
            _db.Locations.Add(location);
            return Save();
        }

        public bool DeleteLocationById(int id)
        {
            var location = _db.Locations.FirstOrDefault(x => x.Id == id);
            if (location != null)
            {
                _db.Locations.Remove(location);
            }
            return Save();
        }

        public ICollection<Location> GetAllLocations()
        {
            return _db.Locations.ToList();
        }

        public Location? GetLocationById(int id)
        {
            return _db.Locations.FirstOrDefault(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLocation(Location location)
        {
            throw new NotImplementedException();
        }
    }
}
