using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Repositories.Abstract;
using FurniMove.Services.Abstract;
using FurniMove.Mapper;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<MoveRequest> GetMoveRequest(int Id)
        {
            var moveRequest = await _moveRequestRepo.GetMoveRequestByIdAsync(Id);
            return moveRequest;
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
            if (result) return moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO, null);
            else return null;
        }

        public async Task<MoveRequestReadDTO?> GetMoveRequestDTOById(int id)
        {
            var moveRequest = await _moveRequestRepo.GetMoveRequestByIdAsync(id);

            if (moveRequest == null) return null;

            var startLocation = await _locationService.GetLocationById((int)moveRequest.startLocationId!);
            var endLocation = await _locationService.GetLocationById((int)moveRequest.endLocationId!);
            var customer = await _userManager.FindByIdAsync(moveRequest.customerId!);
            var customerDTO = _mapper.Map<UserDTO>(customer);
            var moveRequestDTO = new MoveRequestReadDTO();

            if (moveRequest.serviceProviderId != null)
            {
                var serviceProvider = await _userManager.FindByIdAsync(moveRequest.serviceProviderId);
                var serviceProviderDTO = _mapper.Map<UserDTO>(serviceProvider);
                moveRequestDTO = moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO, serviceProviderDTO);
            } else
            {
                moveRequestDTO = moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO, null);
            }

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

                if (moveRequest.serviceProviderId != null)
                {
                    var serviceProvider = await _userManager.FindByIdAsync(moveRequest.serviceProviderId);
                    var serviceProviderDTO = _mapper.Map<UserDTO>(serviceProvider);
                    var moveRequestDTO = moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO, serviceProviderDTO);
                    moveRequestDTOs.Add(moveRequestDTO);
                }
                else
                {
                    var moveRequestDTO = moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO, null);
                    moveRequestDTOs.Add(moveRequestDTO);
                }
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

                if (moveRequest.serviceProviderId != null)
                {
                    var serviceProvider = await _userManager.FindByIdAsync(moveRequest.serviceProviderId);
                    var serviceProviderDTO = _mapper.Map<UserDTO>(serviceProvider);
                    var moveRequestDTO = moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO, serviceProviderDTO);
                    moveRequestDTOs.Add(moveRequestDTO);
                }
                else
                {
                    var moveRequestDTO = moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO, null);
                    moveRequestDTOs.Add(moveRequestDTO);
                }

            }

            return moveRequestDTOs;
        }

        public async Task<List<MoveRequestReadDTO>> GetMoveRequestsByServiceProvider(string serviceProviderId)
        {
            var moveRequests = await _moveRequestRepo.GetMoveRequestsByServiceProvider(serviceProviderId);

            var moveRequestDTOs = new List<MoveRequestReadDTO>();

            foreach (var moveRequest in moveRequests)
            {
                var startLocation = await _locationService.GetLocationById((int)moveRequest.startLocationId);
                var endLocation = await _locationService.GetLocationById((int)moveRequest.endLocationId);
                var customer = await _userManager.FindByIdAsync(moveRequest.customerId);
                var customerDTO = _mapper.Map<UserDTO>(customer);

                if (moveRequest.serviceProviderId != null)
                {
                    var serviceProvider = await _userManager.FindByIdAsync(moveRequest.serviceProviderId);
                    var serviceProviderDTO = _mapper.Map<UserDTO>(serviceProvider);
                    var moveRequestDTO = moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO, serviceProviderDTO);
                    moveRequestDTOs.Add(moveRequestDTO);
                }
                else
                {
                    var moveRequestDTO = moveRequest.ToMoveRequestDTO(startLocation, endLocation, customerDTO, null);
                    moveRequestDTOs.Add(moveRequestDTO);
                }
            }
            return moveRequestDTOs;
        }

        public async Task<bool> RateMove(int MoveId, int Rate)
        {
            var move = await _moveRequestRepo.GetMoveRequestByIdAsync(MoveId);
            if (move == null)
            {
                return false;
            }
            move.rating = Rate;
            var result = await _moveRequestRepo.UpdateMoveRequestAsync(move);
            return result;
        }

        public async Task<bool> UpdateMoveRequest(MoveRequest moveRequest)
        {
            var result = await _moveRequestRepo.UpdateMoveRequestAsync(moveRequest); 
            return result;
        }

        public async Task<bool> StartMove(int moveId)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time"); // UTC+3
            DateTime utcNow = DateTime.UtcNow;
            DateTime utcPlus3Now = TimeZoneInfo.ConvertTime(utcNow, timeZone);
            DateOnly today = DateOnly.FromDateTime(utcPlus3Now);

            var move = await _moveRequestRepo.GetMoveRequestByIdAsync(moveId);
            if (move == null || move.status != "Waiting" || move.startDate != today)
            {
                return false;
            }
            move.status = "Ongoing";
            var result = await _moveRequestRepo.UpdateMoveRequestAsync(move);
            return result;
        }

        public async Task<bool> EndMove(int moveId)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time"); // UTC+3
            DateTime utcNow = DateTime.UtcNow;
            DateTime utcPlus3Now = TimeZoneInfo.ConvertTime(utcNow, timeZone);
            DateOnly today = DateOnly.FromDateTime(utcPlus3Now);

            var move = await _moveRequestRepo.GetMoveRequestByIdAsync(moveId);
            if (move == null || move.status != "Ongoing" || move.startDate != today)
            {
                return false;
            }
            move.status = "Completed";
            var result = await _moveRequestRepo.UpdateMoveRequestAsync(move);
            return result;
        }

        public async Task<MoveRequestReadDTO?> GetOngoingMove(string serviceProviderId)
        {
            var movesDTO = await GetMoveRequestsByServiceProvider(serviceProviderId);

            foreach (var item in movesDTO)
            {
                if (item.Status == "Ongoing") return item;
            }
            return null;
        }
    }
}
