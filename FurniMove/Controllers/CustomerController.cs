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
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMoveRequestService _moveRequestService;
        private readonly IHttpContextAccessor _http;
        private readonly IMoveOfferService _moveOfferService;
        private readonly ILocationService _locationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IApplianceService _applianceService;
        private readonly IMapService _mapService;
        private readonly ITruckService _truckService;

        public CustomerController(IMapper mapper, IMoveRequestService moveRequestService, 
            IHttpContextAccessor httpContextAccessor, IMoveOfferService moveOfferService,
            ILocationService locationService, UserManager<AppUser> userManager,
            IMapService mapService, IApplianceService applianceService, ITruckService truckService) 
        {
            _mapper = mapper;
            _moveRequestService = moveRequestService;
            _http = httpContextAccessor;
            _moveOfferService = moveOfferService;
            _locationService = locationService;
            _userManager = userManager;
            _applianceService = applianceService;
            _mapService = mapService;
            _truckService = truckService;
        }

        [HttpPost("CreateLocation")]
        public async Task<IActionResult> CreateLocation(LocationWriteDTO locationWriteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var location = _mapper.Map<Location>(locationWriteDTO);
            var time = DateTime.UtcNow;
            location.timeStamp = time.AddHours(3);

            var result = await _locationService.CreateLocation(location);
            if (result)
                return Created(nameof(CreateLocation), location);
            return BadRequest(ModelState);
        }

        [HttpPost("CreateMoveRequest")]
        public async Task<IActionResult> CreateMoveRequest(MoveRequestWriteDTO moveRequestWriteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (moveRequestWriteDTO.numOfAppliances > 0)
            {
                var moveRequestReadDTO = await _moveRequestService.CreateMoveRequest(moveRequestWriteDTO, userId);
                if (moveRequestReadDTO != null)
                {
                    user.MoveCounter++;
                    await _userManager.UpdateAsync(user);
                    return Created(nameof(CreateMoveRequest), moveRequestReadDTO);
                }
                return BadRequest("User already has an ongoing move request");
            }
            return BadRequest();
        }

        [HttpGet("GetAddress")]
        public async Task<IActionResult> GetAddress(int locationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _locationService.GetLocationById(locationId);

            if (location == null) return NotFound();

            var address = await _mapService.GetAddress(location.latitude, location.longitude);
            return Ok(address);
        }

        [HttpGet("GetOffers")]
        public async Task<IActionResult> GetOffers()
        {
            var userId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var request = await _moveRequestService.GetMoveRequestByUserId(userId);
            if (request != null)
            {
                return Ok(await _moveOfferService.GetAllMoveOffersByRequestId(request.Id));
            }
            return NotFound();
        }

        [HttpPost("AddAppliance")]
        public async Task<IActionResult> AddAppliance(ApplianceWriteDTO dto)
        {
            var applianceReadDTO = await _applianceService.CreateAppliance(dto, $"{Request.Scheme}://{Request.Host}/Uploads/{dto.moveRequestId}");
            if (applianceReadDTO != null)
            {
                return Ok(applianceReadDTO);
            }
            return Ok("No MoveRequest found!");
        }

        [HttpGet("GetTruckLocation")]
        public async Task<IActionResult> GetTruckLocation(int Id)
        {
            var location = await _truckService.GetTruckLocation(Id);
            if (location != null)
            {
                return Ok(location);
            }
            return NotFound("Truck cannot be located!");
        }

        [HttpPut("RateMove")]
        public async Task<IActionResult> RateMove(int MoveId, int Rate)
        {
            var result = await _moveRequestService.RateMove(MoveId, Rate);
            if (result) return Ok();
            return NotFound();
        }
    }
}
