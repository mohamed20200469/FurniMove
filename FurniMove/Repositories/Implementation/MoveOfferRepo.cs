using FurniMove.Data;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

namespace FurniMove.Repositories.Implementation
{
    public class MoveOfferRepo : IMoveOfferRepo
    {
        private readonly AppDbContext _db;
        public MoveOfferRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateMoveOffer(MoveOffer moveOffer)
        {
            var moveRequest = await _db.MoveRequests.FirstOrDefaultAsync(x => x.Id == moveOffer.MoveRequestId);
            if (moveRequest != null)
            {
                await _db.MoveOffers.AddAsync(moveOffer);
            }
            return await Save();
        }

        public async Task<bool> DeleteMoveOfferById(int id)
        {
            var moveOffer = await _db.MoveOffers.FirstOrDefaultAsync(x => x.Id == id);
            if (moveOffer != null)
            {
                _db.MoveOffers.Remove(moveOffer);
            }
            return await Save();
        }

        public async Task<List<MoveOffer>> GetAllMoveOffers()
        {
            return await _db.MoveOffers.ToListAsync();
        }

        public async Task<List<MoveOffer>?> GetAllMoveOffersByRequestID(int id)
        {
            var request = await _db.MoveRequests.FirstOrDefaultAsync(o => o.Id == id);
            if (request != null)
            {
                var offers = await _db.MoveOffers.Where(x => x.MoveRequestId == id).ToListAsync();
                return offers;
            }
            return null;
        }

        public async Task<MoveOffer?> GetMoveOfferById(int id)
        {
            return await _db.MoveOffers.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = await _db.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateMoveOffer(MoveOffer moveOffer)
        {
            _db.MoveOffers.Update(moveOffer);
            return await Save();
        }

        public async Task<List<MoveOffer>> GetAllMoveOffersByServiceProvider(string serviceProviderId)
        {
            var offers = await _db.MoveOffers.Where(x => x.ServiceProviderId == serviceProviderId).ToListAsync();
            return offers;
        }

        public async Task DeleteNonAcceptedOffers(int moveId)
        {
            var offers = await _db.MoveOffers.Where(x => x.MoveRequestId == moveId && x.Accepted == false).ToListAsync();

            foreach (var item in offers)
            {
                _db.Remove(item);
            }
            await _db.SaveChangesAsync();
        }
    }
}
