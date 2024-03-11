using FurniMove.Models;

namespace FurniMove.Interfaces.IServices
{
    public interface IMoveRequestService
    {
        public bool CreateMoveRequest(MoveRequest moveRequest);
        public ICollection<MoveRequest> GetMoveRequests();
        public MoveRequest? GetMoveRequestById(int id);
    }
}
