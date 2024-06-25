using FurniMove.DTOs;
using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface IMoveOfferService
    {
        public Task<bool> CreateMoveOffer(MoveOffer moveOffer);
        Task<bool> DeleteMoveOfferById(int Id);
        public Task<List<MoveOffer>> GetAllMoveOffers();
        public Task<List<MoveOfferReadDTO>?> GetAllMoveOffersByRequestId(int id);
        Task<List<MoveOfferReadDTO>> GetAllMoveOffersByServiceProvider(string serviceProviderId);
        public Task<MoveOffer?> GetMoveOfferById(int id);
    }
}
