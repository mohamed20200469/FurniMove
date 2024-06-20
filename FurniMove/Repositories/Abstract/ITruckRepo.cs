using FurniMove.Models;

namespace FurniMove.Repositories.Abstract
{
    public interface ITruckRepo
    {
        public Task<Truck?> GetTruckById(int truckId);
        public Task<bool> UpdateTruck(Truck truck);
        public Task<bool> DeleteTruckById(int truckId);
        public Task<bool> CreateTruck(Truck truck);
        public Task<ICollection<Truck>> GetAllTrucks();
        public Task<bool> Save();
        Task<bool> CheckAvailable(string ServiceProviderId, string VehicleType);
    }
}
