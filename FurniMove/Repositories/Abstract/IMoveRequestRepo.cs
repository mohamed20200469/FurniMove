using FurniMove.Models;

namespace FurniMove.Repositories.Abstract
{
    public interface IMoveRequestRepo
    {
        public Task<MoveRequest?> GetMoveRequestByIdAsync(int id);
        public Task<ICollection<MoveRequest>> GetMoveRequestsByStatus(string status);
        public Task<ICollection<MoveRequest>> GetAllMoveRequests();
        public Task<bool> CreateMoveRequestAsync(MoveRequest moveRequest);
        public Task<bool> DeleteMoveRequestByIdAsync(int id);
        public Task<bool> UpdateMoveRequestAsync(MoveRequest moveRequest);
        public Task<bool> SaveAsync();
        public Task<MoveRequest?> GetUserCreatedRequest(string userId);
    }
}
