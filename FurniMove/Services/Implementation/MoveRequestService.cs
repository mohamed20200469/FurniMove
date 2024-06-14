using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using FurniMove.Services.Abstract;
using FurniMove.Mapper;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace FurniMove.Services.Implementation
{
    public class MoveRequestService : IMoveRequestService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILocationService _locationService;
        private readonly IMoveRequestRepo _moveRequestRepo;
        private readonly IMapService _mapService;

        public MoveRequestService(IMoveRequestRepo moveRequestRepo, ILocationService locationService,
            UserManager<AppUser> userManager, IMapper mapper, IMapService mapService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _locationService = locationService;
            _moveRequestRepo = moveRequestRepo;
            _mapService = mapService;
        }

        public async Task<MoveRequestReadDTO?> CreateMoveRequest(MoveRequestWriteDTO moveRequestDTO, string userId)
        {
            var request = await _moveRequestRepo.GetUserCreatedRequest(userId);
            if (request != null) return null;
            var moveRequest = moveRequestDTO.ToMoveRequest();
            moveRequest.customerId = userId;
            var customer = await _userManager.FindByIdAsync(moveRequest.customerId!);
            var customerDTO = _mapper.Map<UserDTO>(customer);
            moveRequest.status = "Created";
            var startLocation = await _locationService.GetLocationById((int)moveRequest.startLocationId!);
            var endLocation = await _locationService.GetLocationById((int)moveRequest.endLocationId!);
            if (startLocation != null && endLocation != null)
            {
                moveRequest.StartAddress = await _mapService.GetAddress(startLocation.latitude, startLocation.longitude);
                moveRequest.EndAddress = await _mapService.GetAddress(endLocation.latitude, endLocation.longitude);
                var (distanceInKm, durationInMinutes) = await _mapService.GetDistanceAndEta(startLocation.latitude, startLocation.longitude, endLocation.latitude, endLocation.longitude);
                moveRequest.Distance = distanceInKm;
                moveRequest.ETA = (float)durationInMinutes;
            }
            else return null;
            var result = await _moveRequestRepo.CreateMoveRequestAsync(moveRequest);
            if (result) return moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO);
            else return null;
        }

        public async Task<MoveRequestReadDTO?> GetMoveRequestById(int id)
        {
            var moveRequest = await _moveRequestRepo.GetMoveRequestByIdAsync(id);

            if (moveRequest == null) return null;

            var startLocation = await _locationService.GetLocationById((int)moveRequest.startLocationId!);
            var endLocation = await _locationService.GetLocationById((int)moveRequest.endLocationId!);
            var customer = await _userManager.FindByIdAsync(moveRequest.customerId!);
            var customerDTO = _mapper.Map<UserDTO>(customer);

            var moveRequestDTO = moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO);
            return moveRequestDTO;
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

                var moveRequestDTO = moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO);

                moveRequestDTOs.Add(moveRequestDTO);
            }
            return moveRequestDTOs;
        }

        public async Task<List<MoveRequestReadDTO>> GetAllMoveRequests()
        {
            var moveRequests = await _moveRequestRepo.GetAllMoveRequests();

            var moveRequestDTOs = new List<MoveRequestReadDTO>();

            foreach (var moveRequest in moveRequests)
            {
                var startLocation = await _locationService.GetLocationById((int)moveRequest.startLocationId);
                var endLocation = await _locationService.GetLocationById((int)moveRequest.endLocationId);
                var customer = await _userManager.FindByIdAsync(moveRequest.customerId);
                var customerDTO = _mapper.Map<UserDTO>(customer);

                var moveRequestDTO = moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO);

                moveRequestDTOs.Add(moveRequestDTO);
            }

            return moveRequestDTOs;
        }
    }
}
