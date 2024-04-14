using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface IMoveRequestService
    {
        public Task<bool> CreateMoveRequest(MoveRequest moveRequest);
        public Task<ICollection<MoveRequest>> GetMoveRequests();
        public Task<MoveRequest?> GetMoveRequestById(int id);
    }
}
