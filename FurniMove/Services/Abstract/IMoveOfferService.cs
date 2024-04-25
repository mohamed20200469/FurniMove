using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface IMoveOfferService
    {
        public Task<bool> CreateMoveOffer(MoveOffer moveOffer);
        public Task<ICollection<MoveOffer>> GetAllMoveOffers();
        public Task<ICollection<MoveOffer>?> GetAllMoveOffersByRequestId(int id);
        public Task<MoveOffer?> GetMoveOfferById(int id);
    }
}
