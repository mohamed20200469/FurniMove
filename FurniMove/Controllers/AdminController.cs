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


        [HttpGet("getMoveRequestsByStatus")]
        public async Task<IActionResult> GetMoveRequestsByStatus(string status)
        {
            try
            {
                var result = await _moveRequestService.GetMoveRequestsByStatus(status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllMoveRequests")]
        public async Task<IActionResult> GetAllMoveRequests()
        {
            try
            {
                var moveRequestDTOs = await _moveRequestService.GetAllMoveRequests();
                return Ok(moveRequestDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getMoveRequestById")]
        public async Task<IActionResult> GetMoveRequestById(int id)
        {
            try
            {
                var moveRequestDTO = await _moveRequestService.GetMoveRequestDTOById(id);
                if (moveRequestDTO == null) return NotFound();
                return Ok(moveRequestDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getOffersByRequestId")]
        public async Task<IActionResult> GetAllMoveOffers(int moveRequestId)
        {
            try
            {
                var moveOfferDTOs = await _moveOfferService.GetAllMoveOffersByRequestId(moveRequestId);
                return Ok(moveOfferDTOs);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            var list = await _userManager.GetUsersInRoleAsync(role);
            var userlist = _mapper.Map<ICollection<UserDTO>>(list);
            return Ok(userlist);
        }

        [HttpPatch("suspendUser")]
        public async Task<IActionResult> SuspendUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            user.Suspended = true;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<UserDTO>(user));
            return BadRequest(result.Errors);
        }

        [HttpPatch("unsuspendUser")]
        public async Task<IActionResult> UnSuspendUser(string userId)
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
