using FurniMove.DTOs;
using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface ITruckService
    {
        public Task<Truck?> GetTruckById(int truckId);
        public Task<bool> UpdateTruck(TruckWriteDTO truck, string ServiceProviderId);
        public Task<bool> DeleteTruckById(int truckId);
        public Task<bool> CreateTruck(Truck truck);
        public Task<ICollection<Truck>> GetAllTrucks();
        Task<bool> CheckAvailable(string serviceProviderId, string VehicleType, DateOnly date);
        Task<Location?> UpdateOrAddTruckLocation(string serviceProviderId, LocationWriteDTO locationDTO);
        Task<Location?> GetTruckLocation(int Id);
        Task<Truck?> GetTruckBySP(string ServiceProviderId);
        Task<(bool, string)> ValidateAsync(TruckWriteDTO dto);
    }
}
