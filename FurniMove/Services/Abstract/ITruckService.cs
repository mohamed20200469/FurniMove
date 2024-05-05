using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface ITruckService
    {
        public Task<Truck?> GetTruckById(int truckId);
        public Task<bool> UpdateTruck(Truck truck);
        public Task<bool> DeleteTruckById(int truckId);
        public Task<bool> CreateTruck(Truck truck);
        public Task<ICollection<Truck>> GetAllTrucks();
    }
}
