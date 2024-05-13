using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurniMove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMoveRequestService _moveRequestService;
        private readonly IMoveOfferService _moveOfferService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileService _fileService;
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        public AdminController(IMoveRequestService moveRequestService, IMapper mapper,
            IMoveOfferService moveOfferService, UserManager<AppUser> userManager,
            IFileService fileService, ILocationService locationService)
        {
            _moveRequestService = moveRequestService;
            _mapper = mapper;
            _moveOfferService = moveOfferService;
            _userManager = userManager;
            _fileService = fileService;
            _locationService = locationService;
        }

 
        [HttpGet("getMoveRequestsByStatus")]
        public async Task<IActionResult> GetMoveRequestsByStatus(string status)
        {
            var moveRequests = await _moveRequestService.GetMoveRequestsByStatus(status);

            var moveRequestDTOs = new List<MoveRequestReadDTO>();

            foreach (var moveRequest in moveRequests)
            {
                var startLocation = await _locationService.GetLocationById((int)moveRequest.startLocationId);
                var endLocation = await _locationService.GetLocationById((int)moveRequest.endLocationId);
                var customer = await _userManager.FindByIdAsync(moveRequest.customerId);
                var customerDTO = _mapper.Map<UserDTO>(customer);

                var moveRequestDTO = new MoveRequestReadDTO
                {
                    Id = moveRequest.Id,
                    StartLocation = startLocation,
                    EndLocation = endLocation,
                    CustomerId = moveRequest.customerId,
                    Customer = customerDTO,
                    Status = moveRequest.status,
                    StartTime = moveRequest.startTime,
                    EndTime = moveRequest.endTime,
                    Rating = moveRequest.rating,
                    Cost = moveRequest.cost,
                    NumOfAppliances = moveRequest.numOfAppliances,
                };

                moveRequestDTOs.Add(moveRequestDTO);
            }

            return Ok(moveRequestDTOs);
        }

        [HttpGet("getAllMoveRequests")]
        public async Task<IActionResult> GetAllMoveRequests()
        {
            var moveRequests = await _moveRequestService.GetAllMoveRequests();

            var moveRequestDTOs = new List<MoveRequestReadDTO>();

            foreach (var moveRequest in moveRequests)
            {
                var startLocation = await _locationService.GetLocationById((int)moveRequest.startLocationId);
                var endLocation = await _locationService.GetLocationById((int)moveRequest.endLocationId);
                var customer = await _userManager.FindByIdAsync(moveRequest.customerId);
                var customerDTO = _mapper.Map<UserDTO>(customer);

                var moveRequestDTO = new MoveRequestReadDTO
                {
                    Id = moveRequest.Id,
                    StartLocation = startLocation,
                    EndLocation = endLocation,
                    CustomerId = moveRequest.customerId,
                    Customer = customerDTO,
                    Status = moveRequest.status,
                    StartTime = moveRequest.startTime,
                    EndTime = moveRequest.endTime,
                    Rating = moveRequest.rating,
                    Cost = moveRequest.cost,
                    NumOfAppliances = moveRequest.numOfAppliances,
                };

                moveRequestDTOs.Add(moveRequestDTO);
            }

            return Ok(moveRequestDTOs);
        }


        [HttpGet("getMoveRequestById")]
        public async Task<IActionResult> GetMoveRequestById(int id)
        {
            var moveRequest = await _moveRequestService.GetMoveRequestById(id);
            if (moveRequest == null) return NotFound();
            
            var startLocation = await _locationService.GetLocationById((int)moveRequest.startLocationId);
            var endLocation = await _locationService.GetLocationById((int)moveRequest.endLocationId);
            var customer = await _userManager.FindByIdAsync(moveRequest.customerId);
            var customerDTO = _mapper.Map<UserDTO>(customer);

            var moveRequestDTO = new MoveRequestReadDTO
            {
                Id = moveRequest.Id,
                StartLocation = startLocation,
                EndLocation = endLocation,
                CustomerId = moveRequest.customerId,
                Customer = customerDTO,
                Status = moveRequest.status,
                StartTime = moveRequest.startTime,
                EndTime = moveRequest.endTime,
                Rating = moveRequest.rating,
                Cost = moveRequest.cost,
                NumOfAppliances = moveRequest.numOfAppliances,
            };
            return Ok(moveRequestDTO);
        }

        [HttpGet("getOffersByRequestId")]
        public async Task<IActionResult> GetAllMoveOffers(int moveRequestId)
        {
            var moveOffers = await _moveOfferService.GetAllMoveOffersByRequestId(moveRequestId);

            var moveOfferDTOs = new List<MoveOfferReadDTO>();

            foreach (var moveOffer in moveOffers)
            {
                var serviceProvider = await _userManager.FindByIdAsync(moveOffer.serviceProviderId);
                var serviceProviderDTO = _mapper.Map<UserDTO>(serviceProvider);

                var moveOfferDTO = new MoveOfferReadDTO
                {
                    Id = moveOffer.Id,
                    ServiceProviderId = moveOffer.serviceProviderId,
                    ServiceProvider = serviceProviderDTO,
                    Price = moveOffer.price,
                    MoveRequestId = moveOffer.moveRequestId
                };

                moveOfferDTOs.Add(moveOfferDTO);
            }
            return Ok(moveOfferDTOs);
        }

        [HttpGet("user={id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("deleteImg")]
        public async Task<IActionResult> DeleteImg(string imageFileName, string folder)
        {
            var res = await _fileService.DeleteImage(imageFileName, folder);
            if (res) return Ok();
            return NotFound();
        }

        [HttpGet("getUsersByRole")]
        public async Task<IActionResult> getUsersByRole(string role)
        {
            var list = await _userManager.GetUsersInRoleAsync(role);
            var userlist = _mapper.Map<ICollection<UserDTO>>(list);
            return Ok(userlist);
        }

        [HttpPatch("suspendUser")]
        public async Task<IActionResult> suspendUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            user.Suspended = true;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<UserDTO>(user));
            return BadRequest(result.Errors);
        }

        [HttpPatch("unsuspendUser")]
        public async Task<IActionResult> unsuspendUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            user.Suspended = false;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<UserDTO>(user));
            return BadRequest(result.Errors);
        }
    }   
}
