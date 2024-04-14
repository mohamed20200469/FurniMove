using FurniMove.Data;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;

namespace FurniMove.Repositories.Implementation
{
    public class MoveOfferRepo : IMoveOfferRepo
    {
        private readonly AppDbContext _db;
        public MoveOfferRepo(AppDbContext db)
        {
            _db = db;
        }
        public bool CreateMoveOffer(MoveOffer moveOffer)
        {
            var moveRequest = _db.MoveRequests.FirstOrDefault(x => x.Id == moveOffer.moveRequestId);
            if (moveRequest != null)
            {
                _db.MoveOffers.Add(moveOffer);
            }
            return Save();
        }

        public bool DeleteMoveOfferById(int id)
        {
            var moveOffer = _db.MoveOffers.FirstOrDefault(x => x.Id == id);
            if (moveOffer != null)
            {
                _db.MoveOffers.Remove(moveOffer);
            }
            return Save();
        }

        public ICollection<MoveOffer> GetAllMoveOffers()
        {
            return _db.MoveOffers.ToList();
        }

        public MoveOffer? GetMoveOfferById(int id)
        {
            return _db.MoveOffers.FirstOrDefault(o => o.Id == id);
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMoveOffer(MoveOffer moveOffer)
        {
            _db.MoveOffers.Update(moveOffer);
            return Save();
        }
    }
}
