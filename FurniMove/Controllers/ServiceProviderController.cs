using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public ServiceProviderController(IMoveOfferService moveOfferService,
            IMapper mapper, IHttpContextAccessor httpContextAccessor, ITruckService truckService)
        {
            _moveOfferService = moveOfferService;
            _mapper = mapper;
            _http = httpContextAccessor;
            _truckService = truckService;
        }

        [HttpPost("CreateMoveOffer")]
        public async Task<IActionResult> CreateMoveOffer(MoveOfferWriteDTO moveOfferDTO)
        {
            var moveOffer = _mapper.Map<MoveOffer>(moveOfferDTO);
            moveOffer.serviceProviderId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool check = await _moveOfferService.CreateMoveOffer(moveOffer);
            if (check)
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

            if (truckWriteDTO.year > 1980 && truckWriteDTO.year < 2024)
            {
                var Truck = _mapper.Map<Truck>(truckWriteDTO);
                Truck.status = "available";
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
        
    }
}
