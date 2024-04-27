using FurniMove.Data;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FurniMove.Repositories.Implementation
{
    public class LocationRepo : ILocationRepo
    {
        private readonly AppDbContext _db;
        public LocationRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateLocation(Location location)
        {
            await _db.Locations.AddAsync(location);
            return await Save();
        }

        public async Task<bool> DeleteLocationById(int id)
        {
            var location = await _db.Locations.FirstOrDefaultAsync(x => x.Id == id);
            if (location != null)
            {
                _db.Locations.Remove(location);
            }
            return await Save();
        }

        public async Task<ICollection<Location>> GetAllLocations()
        {
            return await _db.Locations.ToListAsync();
        }

        public async Task<Location?> GetLocationById(int id)
        {
            return await _db.Locations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = await _db.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateLocation(Location location)
        {
            _db.Locations.Update(location);
            return await Save();
        }
    }
}
