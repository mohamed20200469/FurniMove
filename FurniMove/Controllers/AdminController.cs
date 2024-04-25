using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IMapper _mapper;
        public AdminController(IMoveRequestService moveRequestService, IMapper mapper,
            IMoveOfferService moveOfferService, UserManager<AppUser> userManager,
            IFileService fileService)
        {
            _moveRequestService = moveRequestService;
            _mapper = mapper;
            _moveOfferService = moveOfferService;
            _userManager = userManager;
            _fileService = fileService;
        }

        [AllowAnonymous]
        [HttpGet("GetAllMoveRequests")]
        public async Task<IActionResult> GetAllMoveRequestsAsync()
        {
            var moveRequests = await _moveRequestService.GetMoveRequests();

            return Ok(moveRequests);
        }

        [HttpGet("GetMoveRequestById")]
        public async Task<IActionResult> GetMoveRequestById(int id)
        {
            var moveRequest = await _moveRequestService.GetMoveRequestById(id);
            if (moveRequest == null) return NotFound();
            return Ok(moveRequest);
        }

        [HttpGet("GetAllMoveOffers")]
        public async Task<IActionResult> GetAllMoveOffersAsync()
        {
            return Ok(await _moveOfferService.GetAllMoveOffers());
        }

        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("DeleteImg")]
        public async Task<IActionResult> DeleteImg(string imageFileName, string folder)
        {
            var res = await _fileService.DeleteImage(imageFileName, folder);
            if (res) return Ok();
            return NotFound();
        }
    }   
}
