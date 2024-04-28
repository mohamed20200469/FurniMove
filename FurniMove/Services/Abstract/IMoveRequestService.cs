using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface IMoveRequestService
    {
        public Task<bool> CreateMoveRequest(MoveRequest moveRequest);
        public Task<ICollection<MoveRequest>> GetMoveRequests(string status);
        public Task<MoveRequest?> GetMoveRequestById(int id);
        public Task<MoveRequest?> GetMoveRequestByUserId(string userId);
    }
}
