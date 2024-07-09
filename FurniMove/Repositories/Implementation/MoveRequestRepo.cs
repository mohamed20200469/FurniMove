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

        public async Task<List<MoveRequest>> GetMoveRequestsByStatus(string status)
        {
            var list = await _db.MoveRequests.Where(x => x.status == status).ToListAsync();
            return list;
        }

        public async Task<List<MoveRequest>> GetMoveRequestsByServiceProvider(string serviceProviderId)
        {
            var list = await _db.MoveRequests.Where(x => x.serviceProviderId == serviceProviderId && x.status == "Completed").ToListAsync();
            return list;
        }

        public async Task<List<MoveRequest>> GetMoveRequestsByCustomer(string customerId)
        {
            var list = await _db.MoveRequests.Where(x => x.customerId == customerId && x.status == "Completed").ToListAsync();
            return list;
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
            var request = await _db.MoveRequests.FirstOrDefaultAsync(x => x.customerId == userId && x.status != "Completed");
            return request;
        }

        public async Task<List<MoveRequest>> GetAllMoveRequests()
        {
            var list = await _db.MoveRequests.ToListAsync();
            return list;
        }

        public async Task<MoveRequest?> GetTodaysMove(string serviceProviderId, DateTime dateTime)
        {
            DateOnly today = DateOnly.FromDateTime(dateTime);
            var move = await _db.MoveRequests.FirstOrDefaultAsync(x => x.serviceProviderId == serviceProviderId && x.startDate == today && x.status != "Completed");
            return move;
        }
    }
}
