using FurniMove.Models;

namespace FurniMove.Interfaces.IRepositories
{
    public interface IMoveRequestRepo
    {
        public Task<MoveRequest?> GetMoveRequestByIdAsync(int id);
        public Task<ICollection<MoveRequest>> GetAllMoveRequestsAsync();
        public Task<bool> CreateMoveRequestAsync(MoveRequest moveRequest);
        public Task<bool> DeleteMoveRequestByIdAsync(int id);
        public Task<bool> UpdateMoveRequestAsync(MoveRequest moveRequest);
        public Task<bool> SaveAsync();
    }
}
