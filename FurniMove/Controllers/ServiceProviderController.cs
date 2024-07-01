using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FurniMove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="ServiceProvider")]
    public class ServiceProviderController : ControllerBase
    {
        private IMoveOfferService _moveOfferService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;
        private readonly ITruckService _truckService;
        private readonly IMoveRequestService _moveRequestService;
        private readonly IApplianceService _applianceService;
        private readonly UserManager<AppUser> _userManager;

        public ServiceProviderController(IMoveOfferService moveOfferService,
            IMapper mapper, IHttpContextAccessor httpContextAccessor, ITruckService truckService,
            IMoveRequestService moveRequestService, IApplianceService applianceService, 
            UserManager<AppUser> userManager)
        {
            _moveOfferService = moveOfferService;
            _mapper = mapper;
            _http = httpContextAccessor;
            _truckService = truckService;
            _moveRequestService = moveRequestService;
            _applianceService = applianceService;
            _userManager = userManager;
        }

        [HttpPost("CreateMoveOffer")]
        public async Task<IActionResult> CreateMoveOffer(MoveOfferWriteDTO moveOfferDTO)
        {
            var moveOffer = _mapper.Map<MoveOffer>(moveOfferDTO);
            var serviceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var serviceProvider = await _userManager.FindByIdAsync(serviceProviderId!);

            if (serviceProvider!.Suspended) return Unauthorized("User is suspended!"); 

            moveOffer.ServiceProviderId = serviceProviderId;

            var moveRequest = await _moveRequestService.GetMoveRequestDTOById(moveOfferDTO.moveRequestId);

            if (!await _truckService.CheckAvailable(moveOffer.ServiceProviderId!, moveRequest!.VehicleType!, moveRequest.StartDate))
            {
                return BadRequest("No suitable truck available!");
            }

            bool result = await _moveOfferService.CreateMoveOffer(moveOffer);
            if (result)
            {
                return Created(nameof(GetMoveOfferById), moveOffer);
            }
            return NotFound();
        }

        [HttpGet("Offer/{id}")]
        public async Task<IActionResult> GetMoveOfferById(int id)
        {
            var offer = await _moveOfferService.GetMoveOfferById(id);
            if (offer == null) { return  NotFound(); }
            return Ok(offer);
        }
        
        [HttpPost("AddTruck")]
        public async Task<IActionResult> AddTruck(TruckWriteDTO truckWriteDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (truckWriteDTO.Year > 1980 && truckWriteDTO.Year < 2024)
            if (truckWriteDTO.Year > 1980 && truckWriteDTO.Year < 2024)
            {
                var Truck = _mapper.Map<Truck>(truckWriteDTO);
                Truck.ServiceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                bool result = await _truckService.CreateTruck(Truck);
                if (result)
                {
                    return Ok(Truck);
                }
                return BadRequest();
            }
            return BadRequest("Invalid year");
        }


        [HttpGet("GetCreatedMoveRequests")]
        public async Task<IActionResult> GetAllCreatedMoveRequests()
        {
            var moveRequests = await _moveRequestService.GetMoveRequestsByStatus("Created");
            return Ok(moveRequests);
        }

        [HttpGet("GetMoveHistory")]
        public async Task<IActionResult> GetAllMovesByServiceProvider()
        {
            var serviceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _moveRequestService.GetMoveRequestsByServiceProvider(serviceProviderId));
        }

        [HttpGet("GetAllMoveOffersByServiceProvider")]
        public async Task<IActionResult> GetAllMoveOffersByServiceProvider()
        {
            var serviceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _moveOfferService.GetAllMoveOffersByServiceProvider(serviceProviderId));
        }

        [HttpPost("UpdateTruckLocation")]
        public async Task<IActionResult> AddTruckLocation(LocationWriteDTO locationDTO)
        {
            var serviceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var location = await _truckService.UpdateOrAddTruckLocation(serviceProviderId, locationDTO);
            return Ok(location);
        }

        [HttpPut("StartMove")]
        public async Task<IActionResult> StartMove(int moveId)
        {
            var serviceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var move = await _moveRequestService.GetMoveRequest(moveId);
            if (move.serviceProviderId != serviceProviderId) return BadRequest();

            var result = await _moveRequestService.StartMove(moveId);
            if (result) return Ok();
            return BadRequest();
        }

        [HttpPut("EndMove")]
        public async Task<IActionResult> EndMove(int moveId)
        {
            var serviceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var move = await _moveRequestService.GetMoveRequest(moveId);
            if (move.serviceProviderId != serviceProviderId) return BadRequest();

            var result = await _moveRequestService.EndMove(moveId);
            if (result) return Ok();
            return BadRequest();
        }

        [HttpDelete("DeleteOffer")]
        public async Task<IActionResult> DeleteOffer(int OfferId)
        {
            var result = await _moveOfferService.DeleteMoveOfferById(OfferId);
            if(result) return Ok("Offer deleted successfully!");
            return Ok("Offer doesn't exist or is already Accepted!");
        }

        [HttpPatch("UpdateTruck")]
        public async Task<IActionResult> UpdateTruck(TruckWriteDTO truck)
        {
            var serviceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await _truckService.UpdateTruck(truck, serviceProviderId);

            if (result) return Ok(truck);
            return NotFound();
        }

        [HttpGet("GetAppliancesByMove")]
        public async Task<IActionResult> GetAppliancesByMove(int moveId)
        {
            try
            {
                var appliances = await _applianceService.GetAllAppliancesByMove(moveId);
                return Ok(appliances);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTruck")]
        public async Task<IActionResult> GetTruck()
        {
            var serviceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var truck = await _truckService.GetTruckBySP(serviceProviderId);

            if (truck == null) return NotFound();

            return Ok(truck);
        }


        [HttpGet("GetTodaysMove")]
        public async Task<IActionResult> GetTodaysMove()
        {
            var serviceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var moveDTO = await _moveRequestService.GetTodaysMove(serviceProviderId!);

            if (moveDTO == null) return NotFound();
            return Ok(moveDTO);
        }
        //[HttpGet("GetOngoingMove")]
        //public async Task<IActionResult> GetOngoingMove()
        //{
        //    var serviceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    var moveDTO = await _moveRequestService.GetOngoingMove(serviceProviderId!);

        //    if (moveDTO == null) return NotFound();
        //    return Ok(moveDTO);
        //}
    }
}
