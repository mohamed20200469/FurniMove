using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Services.Abstract;
using FurniMove.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
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

        public CustomerController(IMapper mapper, IMoveRequestService moveRequestService, 
            IHttpContextAccessor httpContextAccessor, IMoveOfferService moveOfferService) 
        {
            _mapper = mapper;
            _moveRequestService = moveRequestService;
            _http = httpContextAccessor;
            _moveOfferService = moveOfferService;
        }

        [HttpPost("CreateMoveRequest")]
        public async Task<ActionResult<MoveRequest>> CreateMoveRequest(MoveRequestWriteDTO moveRequestDTO)
        {
            var moveRequest = _mapper.Map<MoveRequest>(moveRequestDTO);
            moveRequest.customerId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (moveRequest != null && moveRequest.numOfAppliances > 0)
            {

                if(await _moveRequestService.CreateMoveRequest(moveRequest))
                    return Created(nameof(CreateMoveRequest), moveRequest);
                return BadRequest("User already has an ongoing move request");
            }
            return BadRequest();
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
    }
}
