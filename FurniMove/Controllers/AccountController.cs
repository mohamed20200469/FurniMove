using AutoMapper;
using FurniMove.DTOs;
using FurniMove.Models;
using FurniMove.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;

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
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

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

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPatch("setPhoneNumber")]
        public async Task<IActionResult> SetPhoneNumber(string userId, string phoneNumber)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            string regexPattern = @"^(010|011|012|015)\d{8}$";
            if (Regex.IsMatch(phoneNumber, regexPattern))
            {
                await _userManager.SetPhoneNumberAsync(user, phoneNumber);
                return Ok();
            }
            return BadRequest("Incorrect number");
        }

        [Authorize]
        [HttpPost("sendEmailConfirmation")]
        public async Task<IActionResult> SendEmailConfirmation(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();
            if (user.EmailConfirmed == true) return BadRequest("Email already confirmed");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("confirmEmail", "Account", new {token, email = user.Email}, Request.Scheme);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("FurniMove", "furnimoveproject@gmail.com"));
            message.To.Add(new MailboxAddress("", user.Email));
            message.Subject = "Email Confirmation";

            var bodyHtml = @"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                <meta charset=""UTF-8"">
                <meta http-equiv=""x-ua-compatible"" content=""ie=edge"">
                <title>Email Confirmation</title>
                <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                <style>
                /* Styles for the button */
                .button-container {
                    text-align: center;
                }

                .button-container button {
                    background-color: red;
                    color: white;
                    padding: 20px 40px;
                    border: none;
                    border-radius: 8px;
                    font-size: 20px;
                    cursor: pointer;
                    text-decoration: none;
                }

                .button-container button a {
                    text-decoration: none;
                    color: white;
                }
                </style>
                </head>
                <body style=""background-color: #e9ecef; font-family: Arial, sans-serif;"">

                <!-- start body -->
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                    <!-- start explanation text -->
                    <tr>
                        <td align=""center"" bgcolor=""#ffffff"" style=""padding: 30px 20px;"">
                            <p style=""font-size: 18px; margin-bottom: 20px;"">Please confirm your email address to activate your account. If you didn't request this email, you can safely ignore it.</p>
                        </td>
                    </tr>
                    <!-- end explanation text -->
                    <!-- start button -->
                    <tr>
                        <td align=""center"" bgcolor=""#ffffff"" style=""padding-bottom: 30px;"">
                            <div class=""button-container"">
                                <button>
                                    <a href=""" + confirmationLink + @""">Confirm Email Address</a>
                                </button>
                            </div>
                        </td>
                    </tr>
                    <!-- end button -->
                </table>
                <!-- end body -->

                </body>
                </html>
                ";


            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = bodyHtml;


            message.Body = bodyBuilder.ToMessageBody();

            _emailService.Send(message);

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

        [Authorize(Roles = "Admin, Customer, ServiceProvider")]
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
        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetUser()
        {
            var id = _http.HttpContext?.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}