using FurniMove.DTOs;
using FurniMove.Models;

namespace FurniMove.Mapper
{
    public class MoveRequestMappers
    {
        public static MoveRequestReadDTO ToMoveRequestDTO(this MoveRequest moveRequest)
        {
            return new MoveRequestReadDTO
            {
                Id = moveRequest.Id,
                StartLocation = startLocation,
                EndLocation = endLocation,
                CustomerId = moveRequest.customerId,
                Customer = customerDTO,
                Status = moveRequest.status,
                StartTime = moveRequest.startTime,
                EndTime = moveRequest.endTime,
                Rating = moveRequest.rating,
                Cost = moveRequest.cost,
                NumOfAppliances = moveRequest.numOfAppliances,
            };
        }
    }
}
