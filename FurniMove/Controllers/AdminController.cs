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

 
        [HttpGet("getAllMoveRequests")]
        public async Task<IActionResult> GetAllMoveRequests(string status)
        {
            var moveRequests = await _moveRequestService.GetMoveRequests(status);

            return Ok(moveRequests);
        }

        [HttpGet("getMoveRequestById")]
        public async Task<IActionResult> GetMoveRequestById(int id)
        {
            var moveRequest = await _moveRequestService.GetMoveRequestById(id);
            if (moveRequest == null) return NotFound();
            return Ok(moveRequest);
        }

        [HttpGet("getOffersByRequestId")]
        public async Task<IActionResult> GetAllMoveOffersAsync(int moveRequestId)
        {
            return Ok(await _moveOfferService.GetAllMoveOffersByRequestId(moveRequestId));
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
