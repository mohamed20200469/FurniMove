using FurniMove.Models;

namespace FurniMove.Interfaces.IServices
{
    public interface IMoveOfferService
    {
        public bool CreateMoveOffer(MoveOffer moveOffer);
        public ICollection<MoveOffer> GetAllMoveOffers();
    }
}
