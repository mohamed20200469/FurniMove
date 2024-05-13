using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface IMoveRequestService
    {
        public Task<bool> CreateMoveRequest(MoveRequest moveRequest);
        public Task<ICollection<MoveRequest>> GetMoveRequestsByStatus(string status);
        public Task<ICollection<MoveRequest>> GetAllMoveRequests();
        public Task<MoveRequest?> GetMoveRequestById(int id);
        public Task<MoveRequest?> GetMoveRequestByUserId(string userId);

    }
}
