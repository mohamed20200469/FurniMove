using FurniMove.Data;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FurniMove.Repositories.Implementation
{
    public class TruckRepo : ITruckRepo
    {
        private readonly AppDbContext _db;
        public TruckRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateTruck(Truck truck)
        {
            await _db.Trucks.AddAsync(truck);
            return await Save();
        }

        public async Task<bool> DeleteTruckById(int truckId)
        {
            var truck = await _db.Trucks.FirstOrDefaultAsync(x => x.Id == truckId);
            if (truck != null)
            {
                _db.Trucks.Remove(truck);
            }
            return await Save();
        }

        public async Task<ICollection<Truck>> GetAllTrucks()
        {
            return await _db.Trucks.ToListAsync();
        }

        public async Task<Truck?> GetTruckById(int truckId)
        {
            var truck = await _db.Trucks.FirstOrDefaultAsync(x => x.Id == truckId);
            return truck;
        }

        public async Task<bool> Save()
        {
            var saved = await _db.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateTruck(Truck truck)
        {
            _db.Trucks.Update(truck);
            return await Save();
        }

        public async Task<bool> CheckAvailable(string ServiceProviderId, string VehicleType, DateOnly date)
        {
            var truck = await _db.Trucks.FirstOrDefaultAsync(x => x.ServiceProviderId == ServiceProviderId &&
            x.Type == VehicleType);

            
            if (truck != null)
            {
                var check = await _db.MoveRequests.FirstOrDefaultAsync(y => y.serviceProviderId == ServiceProviderId &&
                y.startDate == date);

                if (check != null)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public async Task<Truck?> GetTuckBySP(string ServiceProviderId)
        {
            var truck = await _db.Trucks.FirstOrDefaultAsync(x => x.ServiceProviderId == ServiceProviderId);
            return truck;
        }
    }
}
