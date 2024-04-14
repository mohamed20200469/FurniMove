using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using FurniMove.Services.Abstract;

namespace FurniMove.Services.Implementation
{
    public class MoveRequestService : IMoveRequestService
    {
        private readonly IMoveRequestRepo _moveRequestRepo;
        public MoveRequestService(IMoveRequestRepo moveRequestRepo)
        {
            _moveRequestRepo = moveRequestRepo;
        }

        public async Task<bool> CreateMoveRequest(MoveRequest moveRequest)
        {
            return await _moveRequestRepo.CreateMoveRequestAsync(moveRequest);
        }

        public async Task<MoveRequest?> GetMoveRequestById(int id)
        {
            return await _moveRequestRepo.GetMoveRequestByIdAsync(id);
        }

        public async Task<ICollection<MoveRequest>> GetMoveRequests()
        {
            return await _moveRequestRepo.GetAllMoveRequestsAsync();
        }
    }
}
