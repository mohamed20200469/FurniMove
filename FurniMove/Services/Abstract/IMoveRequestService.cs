using FurniMove.DTOs;
using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface IMoveRequestService
    {
        public Task<MoveRequestReadDTO?> CreateMoveRequest(MoveRequestWriteDTO moveRequest, string userId);
        public Task<List<MoveRequestReadDTO>> GetMoveRequestsByStatus(string status);
        public Task<List<MoveRequestReadDTO>> GetAllMoveRequests();
        public Task<MoveRequestReadDTO?> GetMoveRequestById(int id);
        public Task<MoveRequest?> GetMoveRequestByUserId(string userId);

    }
}
