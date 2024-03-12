using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Interfaces.IServices;
using FurniMove.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IMapper _mapper;
        public AdminController(IMoveRequestService moveRequestService, IMapper mapper, IMoveOfferService moveOfferService)
        {
            _moveRequestService = moveRequestService;
            _mapper = mapper;
            _moveOfferService = moveOfferService;

        }
        // Hello there
        [AllowAnonymous]
        [HttpGet("GetAllMoveRequests")]
        public ActionResult<ICollection<MoveRequest>> GetAllMoveRequests()
        {
            var moveRequests = _moveRequestService.GetMoveRequests();

            return Ok(moveRequests);
        }

        [HttpGet("GetMoveRequestById")]
        public ActionResult<MoveRequest> GetMoveRequestById(int id)
        {
            var moveRequest = _moveRequestService.GetMoveRequestById(id);
            if (moveRequest == null) return NotFound();
            return Ok(moveRequest);
        }

        [HttpGet("GetAllMoveOffers")]
        public ActionResult<ICollection<MoveOffer>> GetAllMoveOffers()
        {
            return Ok(_moveOfferService.GetAllMoveOffers());
        }

        [HttpPost("CreateMoveRequest")]
        public ActionResult<MoveRequest> CreateMoveRequest(MoveRequestWriteDTO moveRequestDTO)
        {
            var moveRequest = _mapper.Map<MoveRequest>(moveRequestDTO);
            if (moveRequest != null && moveRequest.numOfAppliances > 0)
            {
                moveRequest.status = "Created";
                _moveRequestService.CreateMoveRequest(moveRequest);
                return CreatedAtAction("GetMoveRequestById", moveRequest);
            }
            return BadRequest();
        }

        [HttpPost("CreateMoveOffer")]
        public ActionResult<MoveOffer> CreateMoveOffer(MoveOfferWriteDTO moveOfferDTO)
        {
            var moveOffer = _mapper.Map<MoveOffer>(moveOfferDTO);
            bool check = _moveOfferService.CreateMoveOffer(moveOffer);
            if (check)
            {
                return CreatedAtAction("GetAllMoveOffers", moveOffer);
            }
            return NotFound();
        }
    }   
}
