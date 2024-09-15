using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RamsTrackerAPI.Models.DTO.AuthDTO;
using RamsTrackerAPI.Repositories.AuthRepository;
using System.Net;
using System.Net.Mail;
using RamsTrackerAPI.Data.Account;

namespace RamsTrackerAPI.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public AuthController(UserManager<User> userManager, 
                              ITokenRepository tokenRepository, 
                              IEmailService emailService,
                              IConfiguration config)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
            this._emailService = emailService;
            this._config = config;
        }
        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var User = new User
            {
                FirstName = registerRequestDTO.FirstName,
                LastName = registerRequestDTO.LastName,
                PhoneNumber = registerRequestDTO.Phone,
                Role = registerRequestDTO.Roles[0],
                Contractor = registerRequestDTO.Contractor,
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            var identityResult = await _userManager.CreateAsync(User, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this User
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(User, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                       var confirmationToken = await this._userManager.GenerateEmailConfirmationTokenAsync(User);

                        await SendConfirmationEmail(User.Email, User, confirmationToken);

                       
                            return Ok(registerRequestDTO);
                    }
                }
            }
            return BadRequest("Something went wrong!");
        }

        [HttpGet("Emailconfirmation")]
        
        public async Task <IActionResult> EmailVerification([FromQuery]string? UserId, [FromQuery] string? Token)
        {
            
            if (UserId == null || Token == null)
                return BadRequest(error: "Invalid payload");

            string token = Token.Replace(" ", "+");

            var user = await _userManager.FindByIdAsync(UserId);
            if(user == null)
                return BadRequest(error: "User do not exist");

            //setting redirect route to login 
            string? routing = _config["Emailsettings:ConfirmationRedirect"];
            var result = Content("<html><title>Email confirmed</title><head><h2>Email is confirmed."
                                  + $"<br/>Please try to <b><a href='{routing}'>Log in</a></b>"
                                  + " </h2></head><body/></html>");
            result.ContentType = "text/html; charset=UTF-8";

            if (await _userManager.IsEmailConfirmedAsync(user))
                return result;
            
            var isVerified = await _userManager.ConfirmEmailAsync(user, token);
            if (isVerified.Succeeded)
                return result;
                
            return BadRequest(error: "We can not verify your email address.");
                
        }

        private async Task SendConfirmationEmail(string? email, User? user, string token)
        {
            var confirmationLink = $"https://localhost:7061/api/Auth/Emailconfirmation?UserId={user?.Id}&&Token={token}";
            await _emailService.SendEmailAsync(email, "Confirm Your Email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>;.", true);
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);

            if (user != null) 
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                    return Unauthorized("Email not confirmed");
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if (checkPasswordResult)
                {
                    //Get the roles for this user
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token 

                       var jwtToken =  _tokenRepository.CreateJwtToken(user, roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Phone = user.PhoneNumber,
                            Contractor = user.Contractor,
                            Username = loginRequestDTO.Username,
                            Roles = roles.ToArray(),
                            JwtToken = jwtToken


                        };
                       return Ok(response);
                    }
                    
                    
                }
            }
            return BadRequest("Username or password incorect");
        }
    }
}
