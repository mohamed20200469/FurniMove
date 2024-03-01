using FurniMove.Models;

namespace FurniMove.Interfaces.IRepositories
{
    public interface IMoveRequestRepo
    {
        public MoveRequest? GetMoveRequestById(int id);
        public ICollection<MoveRequest> GetMoveRequests();
        public bool CreateMoveRequest(MoveRequest moveRequest);
        public bool DeleteMoveRequestById(int id);
        public bool UpdateMoveRequest(MoveRequest moveRequest);
        bool Save();
    }
}
