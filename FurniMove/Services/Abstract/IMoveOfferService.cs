using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface IMoveOfferService
    {
        public bool CreateMoveOffer(MoveOffer moveOffer);
        public ICollection<MoveOffer> GetAllMoveOffers();
    }
}
