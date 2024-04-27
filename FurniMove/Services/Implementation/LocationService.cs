using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using FurniMove.Services.Abstract;

namespace FurniMove.Services.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepo _locationRepo;

        public LocationService(ILocationRepo locationRepo)
        {
            _locationRepo = locationRepo;
        }

        public async Task<bool> CreateLocation(Location location)
        {
            var result = await _locationRepo.CreateLocation(location);
            return result;
        }

        public async Task<bool> DeleteLocationById(int id)
        {
            var result = await _locationRepo.DeleteLocationById(id);
            return result;
        }

        public async Task<bool> UpdateLocation(Location location)
        {
            var result = await _locationRepo.UpdateLocation(location);
            return result;
        }

        public async Task<Location?> GetLocationById(int id)
        {
            return await _locationRepo.GetLocationById(id);
        }

        public async Task<ICollection<Location>> GetAllLocations()
        {
            return await _locationRepo.GetAllLocations();
        }
    }
}
