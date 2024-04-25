using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using FurniMove.Services.Abstract;

namespace FurniMove.Services.Implementation
{
    public class MoveOfferService : IMoveOfferService
    {
        private readonly IMoveOfferRepo _moveOfferRepo;
        public MoveOfferService(IMoveOfferRepo moveOfferRepo)
        {
            _moveOfferRepo = moveOfferRepo;
        }
        public async Task<bool> CreateMoveOffer(MoveOffer moveOffer)
        {
            return await _moveOfferRepo.CreateMoveOffer(moveOffer);
        }

        public async Task<ICollection<MoveOffer>> GetAllMoveOffers()
        {
            return await _moveOfferRepo.GetAllMoveOffers();
        }

        public async Task<ICollection<MoveOffer>?> GetAllMoveOffersByRequestId(int id)
        {
            return await _moveOfferRepo.GetAllMoveOffersByRequestID(id);
        }

        public async Task<MoveOffer?> GetMoveOfferById(int id)
        {
            return await _moveOfferRepo.GetMoveOfferById(id);
        }
    }
}
