using FurniMove.DTOs;
using FurniMove.Models;

namespace FurniMove.Mapper
{
    public static class MoveRequestMappers
    {
        public static MoveRequestReadDTO ToMoveRequestDTO(this MoveRequest moveRequest, Location startLocation,
            Location endLocation, UserDTO customerDTO, UserDTO serviceProviderDTO)
        {
            return new MoveRequestReadDTO
            {
                Id = moveRequest.Id,
                StartLocation = startLocation,
                StartAddress = moveRequest.StartAddress,
                EndLocation = endLocation,
                EndAddress = moveRequest.EndAddress,
                ETA = moveRequest.ETA,
                Distance = moveRequest.Distance,
                CustomerId = moveRequest.customerId,
                Customer = customerDTO,
                ServiceProviderId = moveRequest.serviceProviderId,
                ServiceProvider = serviceProviderDTO,
                Status = moveRequest.status,
                StartDate = moveRequest.startDate,
                EndTime = moveRequest.endTime,
                VehicleType = moveRequest.VehicleType,
                Rating = moveRequest.rating,
                Cost = moveRequest.cost,
                NumOfAppliances = moveRequest.numOfAppliances,
            };
        }

        public static MoveRequest ToMoveRequest(this MoveRequestWriteDTO dto)
        {
            return new MoveRequest
            {
                startLocationId = dto.startLocationId,
                endLocationId = dto.endLocationId,
                numOfAppliances = dto.numOfAppliances,
                startDate = dto.StartDate,
                VehicleType = dto.VehicleType
            };
        }

    }
}
