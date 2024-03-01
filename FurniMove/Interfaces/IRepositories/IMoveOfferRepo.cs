using FurniMove.Models;

namespace FurniMove.Interfaces.IRepositories
{
    public interface IMoveOfferRepo
    {
        public bool CreateMoveOffer(MoveOffer moveOffer);
        public bool DeleteMoveOfferById(int id);
        public bool UpdateMoveOffer(MoveOffer moveOffer);
        public MoveOffer? GetMoveOfferById(int id);
        public ICollection<MoveOffer> GetAllMoveOffers();
        bool Save();
    }
}
