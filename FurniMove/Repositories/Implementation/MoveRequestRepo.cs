using FurniMove.Data;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FurniMove.Repositories.Implementation
{
    public class MoveRequestRepo : IMoveRequestRepo
    {
        private readonly AppDbContext _db;
        public MoveRequestRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateMoveRequestAsync(MoveRequest moveRequest)
        {
            await _db.MoveRequests.AddAsync(moveRequest);
            return await SaveAsync();
        }

        public async Task<bool> DeleteMoveRequestByIdAsync(int id)
        {
            var moveRequest = await _db.MoveRequests.FindAsync(id);
            if (moveRequest != null)
            {
                _db.MoveRequests.Remove(moveRequest);
            }
            return await SaveAsync();
        }

        public async Task<MoveRequest?> GetMoveRequestByIdAsync(int id)
        {
            var moveRequest = await _db.MoveRequests.FindAsync(id);
            return moveRequest;
        }

        public async Task<ICollection<MoveRequest>> GetAllMoveRequestsAsync()
        {
            return await _db.MoveRequests.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _db.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateMoveRequestAsync(MoveRequest moveRequest)
        {
            _db.MoveRequests.Update(moveRequest);
            return await SaveAsync();
        }

        public async Task<MoveRequest?> GetUserCreatedRequest(string userId)
        {
            var request = await _db.MoveRequests.FirstOrDefaultAsync(
                x => x.customerId == userId && x.status != "Completed");
            return request;
        }
    }
}
