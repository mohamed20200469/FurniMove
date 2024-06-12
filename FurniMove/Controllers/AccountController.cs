using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace FurniMove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,
            SignInManager<AppUser> signInManager, IEmailService emailService, IFileService fileService,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _emailService = emailService;
            _fileService = fileService;
            _http = httpContextAccessor;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null) return NotFound("Email not found!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Password incorrect!");

            var userDTO = _mapper.Map<UserDTO>(user);
            userDTO.Token = _tokenService.CreateToken(user);

            return Ok(userDTO);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (await _userManager.FindByEmailAsync(registerDTO.Email) != null) 
                    return BadRequest("Email already in use!");

                if(registerDTO.Role != "Admin" &&
                    registerDTO.Role != "Customer" &&
                    registerDTO.Role != "ServiceProvider") 
                    return BadRequest("Role Doesn't exist!");

                var appUser = new AppUser
                {
                    UserName = registerDTO.Username,
                    Email = registerDTO.Email,
                    PhoneNumber = registerDTO.PhoneNumber,
                    Role = registerDTO.Role
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, appUser.Role);
                    if (roleResult.Succeeded)
                    {
                        var userDTO = _mapper.Map<UserDTO>(appUser);
                        userDTO.Token = _tokenService.CreateToken(appUser);
                        return Ok(userDTO);
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); ;
            }
        }

        [Authorize]
        [HttpPatch("updateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO updateDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = _http.HttpContext?.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound("No JWT token!");
            
            bool result = true;
            
            if (user.PhoneNumber != updateDTO.PhoneNumber)
            {
                var result1 = await _userManager.SetPhoneNumberAsync(user, updateDTO.PhoneNumber);
                result = result1.Succeeded;
            }
            
            if (updateDTO.Password != null)
            {
                var result2 = await _userManager.ChangePasswordAsync(user, updateDTO.OldPassword, updateDTO.Password);
                result = result2.Succeeded;
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            if (result) return Ok(userDTO);

            return BadRequest();
        }

        /*
        [Authorize]
        [HttpPatch("updateUsername")]
        public async Task<IActionResult> ChangeUsername([FromBody] string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _http.HttpContext?.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            await _userManager.SetUserNameAsync(user, username);
            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }
        */

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [Authorize]
        [HttpPost("sendEmailConfirmation")]
        public async Task<IActionResult> SendEmailConfirmation()
        {
            var email = _http.HttpContext?.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();
            if (user.EmailConfirmed == true) return BadRequest("Email already confirmed");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("confirmEmail", "Account", new { token, email = user.Email }, Request.Scheme);

            _emailService.ConfirmEmailAddress(user.Email, confirmationLink);
            return Ok();
        }


        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token,string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null) return NotFound();
            await _userManager.ConfirmEmailAsync(user, token);
            return Ok("Email Confirmed!");
        }

        [Authorize]
        [HttpPost("addUserImg")]
        public async Task<IActionResult> AddUserImg(IFormFile img)
        {
            var fileResult = _fileService.SaveImage(img, "ProfilePictures");
            var id = _http.HttpContext?.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(id);
            if(user == null) return NotFound();
            if(fileResult.Item1 == 1)
            {
                if (user.UserImgURL != null)
                {
                    string lastPart = new Uri(user.UserImgURL).AbsolutePath.TrimEnd('/').Split('/').Last();
                    await _fileService.DeleteImage(lastPart, "ProfilePictures");
                }
                user.UserImgURL = $"{Request.Scheme}://{Request.Host}/Uploads/ProfilePictures/{fileResult.Item2}";
                await _userManager.UpdateAsync(user);
                return Ok();
            }
            return BadRequest();
        }

        [Authorize]
        [HttpDelete("deleteUserImg")]
        public async Task<IActionResult> DeleteUserImg()
        {
            var id = _http.HttpContext?.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("No JWT");
            if (user.UserImgURL != null)
            {
                string lastPart = new Uri(user.UserImgURL).AbsolutePath.TrimEnd('/').Split('/').Last();
                await _fileService.DeleteImage(lastPart, "ProfilePictures");
                user.UserImgURL = null ;
                await _userManager.UpdateAsync(user);
                return Ok();
            }
            return NotFound("No User Img");
        }

        [Authorize]
        [HttpGet("getCurrentUser")]
        public async Task<IActionResult> GetUser()
        {
            var id = _http.HttpContext?.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }
    }
}