using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using FurniMove.Services.Abstract;
using System.Diagnostics;

namespace FurniMove.Services.Implementation
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepo _truckRepo;

        public TruckService(ITruckRepo truckRepo)
        {
            _truckRepo = truckRepo;
        }
        public async Task<bool> CreateTruck(Truck truck)
        {
            return await _truckRepo.CreateTruck(truck);
        }

        public async Task<bool> DeleteTruckById(int truckId)
        {
            return await _truckRepo.DeleteTruckById(truckId);
        }

        public async Task<ICollection<Truck>> GetAllTrucks()
        {
            return await _truckRepo.GetAllTrucks();
        }

        public async Task<Truck?> GetTruckById(int truckId)
        {
            return await _truckRepo.GetTruckById(truckId);
        }

        public async Task<bool> UpdateTruck(Truck truck)
        {
            return await _truckRepo.CreateTruck(truck);
        }

        public async Task<bool> CheckAvailable(string serviceProviderId, string VehicleType)
        {
            return await _truckRepo.CheckAvailable(serviceProviderId, VehicleType);
        }
    }
}
