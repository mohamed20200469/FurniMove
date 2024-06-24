using FurniMove.Models;

namespace FurniMove.Repositories.Abstract
{
    public interface IMoveOfferRepo
    {
        public Task<bool> CreateMoveOffer(MoveOffer moveOffer);
        public Task<bool> DeleteMoveOfferById(int id);
        public Task<bool> UpdateMoveOffer(MoveOffer moveOffer);
        public Task<MoveOffer?> GetMoveOfferById(int id);
        public Task<List<MoveOffer>> GetAllMoveOffers();
        public Task<List<MoveOffer>?> GetAllMoveOffersByRequestID(int id);
        Task<bool> Save();
        Task<List<MoveOffer>> GetAllMoveOffersByServiceProvider(string serviceProviderId);
    }
}
