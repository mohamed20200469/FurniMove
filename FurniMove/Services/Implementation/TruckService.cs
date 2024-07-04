using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using FurniMove.Services.Abstract;
using System.Diagnostics;
using System.Text.RegularExpressions;

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
            truck.Year = (int)truckDTO.Year!;
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

        public async Task<(bool, string)> ValidateAsync(TruckWriteDTO dto)
        {
            return await Task.Run(() =>
            {
                // Validate PlateNumber
                var plateNumberPattern = @"^\d{1,4}[ا-ي]{1,4}$";
                if (string.IsNullOrEmpty(dto.PlateNumber) || !Regex.IsMatch(dto.PlateNumber, plateNumberPattern))
                {
                    return (false, "Plate number is invalid. It should be 1 to 4 digits followed by 1 to 4 Arabic letters.");
                }

                // Validate Year
                if (!dto.Year.HasValue || dto.Year < 1980 || dto.Year > 2024)
                {
                    return (false, "Year is invalid. It should be between 1980 and 2024.");
                }

                // Validate Type
                var validTypes = new List<string> { "Van", "Pickup", "Truck" };
                if (string.IsNullOrEmpty(dto.Type) || !validTypes.Contains(dto.Type))
                {
                    return (false, "Type is invalid. It should be one of the following: Van, Pickup, Truck.");
                }

                // Validate Brand and Model
                var validBrandsAndModels = new Dictionary<string, List<string>>
            {
                { "Toyota", new List<string> { "Hilux", "Hiace" } },
                { "Ford", new List<string> { "F-150", "Ranger", "Transit" } },
                { "Chevrolet", new List<string> { "Silverado", "Express", "Colorado" } },
                { "Mercedes-Benz", new List<string> { "Sprinter", "Vito", "X-Class" } },
                { "Nissan", new List<string> { "Navara", "NV350" } }
            };

                if (string.IsNullOrEmpty(dto.Brand) || !validBrandsAndModels.ContainsKey(dto.Brand))
                {
                    return (false, "Brand is invalid. It should be one of the following: Toyota, Ford, Chevrolet, Mercedes-Benz, Nissan.");
                }

                if (string.IsNullOrEmpty(dto.Model) || !validBrandsAndModels[dto.Brand].Contains(dto.Model))
                {
                    return (false, $"Model is invalid for the brand {dto.Brand}. Valid models are: {string.Join(", ", validBrandsAndModels[dto.Brand])}.");
                }

                return (true, "Validation successful.");
            });
        }
    }
}
