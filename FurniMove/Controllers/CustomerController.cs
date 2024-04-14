using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Services;
using FurniMove.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

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

        public CustomerController(IMapper mapper, IMoveRequestService moveRequestService, 
            IHttpContextAccessor httpContextAccessor) 
        {
            _mapper = mapper;
            _moveRequestService = moveRequestService;
            _http = httpContextAccessor;
        }

        [HttpPost("CreateMoveRequest")]
        public async Task<ActionResult<MoveRequest>> CreateMoveRequest(MoveRequestWriteDTO moveRequestDTO)
        {
            var moveRequest = _mapper.Map<MoveRequest>(moveRequestDTO);
            moveRequest.customerId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (moveRequest != null && moveRequest.numOfAppliances > 0)
            {
                moveRequest.status = "Created";
                await _moveRequestService.CreateMoveRequest(moveRequest);
                return Created(nameof(CreateMoveRequest), moveRequest);
            }
            return BadRequest();
        }

        [HttpGet("GetCurrentUser")]
        public ActionResult<string> GetCurrentUser()
        {
            var name = _http.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            var id = _http.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(name + "\n" + id);
        }
    }
}
