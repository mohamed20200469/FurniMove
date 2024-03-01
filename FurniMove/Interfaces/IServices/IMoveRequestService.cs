using FurniMove.Models;

namespace FurniMove.Interfaces.IServices
{
    public interface IMoveRequestService
    {
        public bool CreateMoveRequest(MoveRequest moveRequest);
        public MoveRequest? GetMoveRequestById(int id);
    }
}
