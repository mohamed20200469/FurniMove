using FurniMove.DTOs;
using FurniMove.Models;

namespace FurniMove.Mapper
{
    public static class MoveOfferMappers
    {
        public static MoveOfferReadDTO ToMoveOfferDTO(this MoveOffer moveOffer, UserDTO serviceProviderDTO)
        {
            return new MoveOfferReadDTO
            {
                Id = moveOffer.Id,
                ServiceProviderId = moveOffer.serviceProviderId,
                ServiceProvider = serviceProviderDTO,
                Price = moveOffer.price,
                MoveRequestId = moveOffer.moveRequestId
            };
        }
    }
}
