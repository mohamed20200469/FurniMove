using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using FurniMove.Repositories.Implementation;
using FurniMove.Services.Abstract;
using System.Diagnostics;

namespace FurniMove.Services.Implementation
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepo _truckRepo;
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;

        public TruckService(ITruckRepo truckRepo, IMapper mapper,
            ILocationService locationService)
        {
            _truckRepo = truckRepo;
            _mapper = mapper;
            _locationService = locationService;
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

        public async Task<bool> UpdateTruck(TruckWriteDTO truckDTO, string ServiceProviderId)
        {
            var truck = await _truckRepo.GetTuckBySP(ServiceProviderId);
            if (truck == null) return false;

            truck.Brand = truckDTO.Brand;
            truck.Year = (int)truckDTO.Year;
            truck.PlateNumber = truckDTO.PlateNumber;
            truck.Model = truckDTO.Model;
            truck.Type = truckDTO.Type;
            
            var result = await _truckRepo.UpdateTruck(truck);
            return result;
        }

        public async Task<bool> CheckAvailable(string serviceProviderId, string VehicleType, DateOnly date)
        {
            return await _truckRepo.CheckAvailable(serviceProviderId, VehicleType, date);
        }

        public async Task<Location?> UpdateOrAddTruckLocation(string serviceProviderId, LocationWriteDTO locationDTO)
        {
            var truck = await _truckRepo.GetTuckBySP(serviceProviderId);
            if (truck == null)
            {
                return null;
            }
            var time = DateTime.UtcNow;
            if (truck.CurrentLocationId != null)
            {
                var location = await _locationService.GetLocationById((int)truck.CurrentLocationId);
                location!.longitude = locationDTO.longitude; 
                location.latitude = locationDTO.latitude;
                location.timeStamp = time.AddHours(3);
                var result = await _locationService.UpdateLocation(location);
                return location;
            }
            var location2 = _mapper.Map<Location>(locationDTO);
            location2.timeStamp = time.AddHours(3);
            var result2 = await _locationService.CreateLocation(location2);
            truck.CurrentLocationId = location2.Id;
            var result3 = await _truckRepo.UpdateTruck(truck);
            return location2;
        }

        public async Task<Location?> GetTruckLocation(int Id)
        {
            var truck = await _truckRepo.GetTruckById(Id);
            if (truck == null || truck.CurrentLocationId == null) return null;
            var location = await _locationService.GetLocationById((int)truck.CurrentLocationId);
            return location;
        }

        public async Task<Truck?> GetTruckBySP(string ServiceProviderId)
        {
            var truck = await _truckRepo.GetTuckBySP(ServiceProviderId);
            return truck;
        }
    }
}
