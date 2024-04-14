using FurniMove.Models;

namespace FurniMove.Interfaces.IServices
{
    public interface IMoveRequestService
    {
        public Task<bool> CreateMoveRequest(MoveRequest moveRequest);
        public Task<ICollection<MoveRequest>> GetMoveRequests();
        public Task<MoveRequest?> GetMoveRequestById(int id);
    }
}
