using FurniMove.DTOs;
using FurniMove.Models;

namespace FurniMove.Services.Abstract
{
    public interface IMoveRequestService
    {
        public Task<MoveRequestReadDTO?> CreateMoveRequest(MoveRequestWriteDTO moveRequest, string userId);
        public Task<List<MoveRequestReadDTO>> GetMoveRequestsByStatus(string status);
        public Task<List<MoveRequestReadDTO>> GetAllMoveRequests();
        public Task<MoveRequestReadDTO?> GetMoveRequestDTOById(int id);
        public Task<MoveRequestReadDTO?> GetMoveRequestDTOByUserId(string userId);
        public Task<List<MoveRequestReadDTO>> GetMoveRequestsByServiceProvider(string serviceProviderId);
        Task<bool> RateMove(int MoveId, int Rate);
        Task<bool> UpdateMoveRequest(MoveRequest moveRequest);
        Task<MoveRequest?> GetMoveRequest(int Id);
        Task<bool> StartMove(int moveId);
        Task<bool> EndMove(int moveId);
        Task<MoveRequestReadDTO?> GetOngoingMove(string serviceProviderId);
        Task<MoveRequestReadDTO?> GetTodaysMove(string serviceProviderId);
        Task<List<MoveRequestReadDTO>> GetCustomerHistory(string customerId);
        Task<MoveRequest?> GetMoveRequestByUserId(string userId);
    }
}
