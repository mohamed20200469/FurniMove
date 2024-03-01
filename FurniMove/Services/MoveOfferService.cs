using FurniMove.Interfaces.IRepositories;
using FurniMove.Interfaces.IServices;
using FurniMove.Models;

namespace FurniMove.Services
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
