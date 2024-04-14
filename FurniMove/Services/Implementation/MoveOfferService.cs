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
        public bool CreateMoveOffer(MoveOffer moveOffer)
        {
            return _moveOfferRepo.CreateMoveOffer(moveOffer);
        }

        public ICollection<MoveOffer> GetAllMoveOffers()
        {
            return _moveOfferRepo.GetAllMoveOffers();
        }
    }
}
