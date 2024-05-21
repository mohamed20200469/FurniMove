using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using FurniMove.Services.Abstract;
using FurniMove.Mapper;

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
            var request = await _moveRequestRepo.GetUserCreatedRequest(moveRequest.customerId);
            if (request != null) return false;
            moveRequest.status = "Created";
            return await _moveRequestRepo.CreateMoveRequestAsync(moveRequest);
        }

        public async Task<MoveRequest?> GetMoveRequestById(int id)
        {
            return await _moveRequestRepo.GetMoveRequestByIdAsync(id);
        }

        public async Task<MoveRequest?> GetMoveRequestByUserId(string userId)
        {
            var request = await _moveRequestRepo.GetUserCreatedRequest(userId);
            return request;
        }

        public async Task<List<MoveRequestReadDTO>> GetMoveRequestsByStatus(string status)
        {
            var moveRequests = await _moveRequestRepo.GetMoveRequestsByStatus(status);

            var moveRequestDTOs = new List<MoveRequestReadDTO>();

            foreach (var moveRequest in moveRequests)
            {
                var startLocation = await _locationService.GetLocationById((int)moveRequest.startLocationId);
                var endLocation = await _locationService.GetLocationById((int)moveRequest.endLocationId);
                var customer = await _userManager.FindByIdAsync(moveRequest.customerId);
                var customerDTO = _mapper.Map<UserDTO>(customer);

                var moveRequestDTO = moveRequest.ToMoveRequestDTO();

                moveRequestDTOs.Add(moveRequestDTO);
            }
            return moveRequestDTOs;
        }

        public async Task<ICollection<MoveRequest>> GetAllMoveRequests()
        {
            return await _moveRequestRepo.GetAllMoveRequests();
        }
    }
}
