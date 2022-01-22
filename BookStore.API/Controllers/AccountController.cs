using BookStore.API.JwtFeatures;
using DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contracts;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly JwtHandler _jwtHandler;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IAccountService accountService, JwtHandler jwtHandler, UserManager<ApplicationUser> userManager)
        {
            _accountService = accountService;
            _jwtHandler = jwtHandler;
            _userManager = userManager;
        }
        [HttpPost]
        public  ActionResult<AuthenticationResponseDTO> Register(RegisterationDTO registerationDTO) {
            return  Ok(_accountService.Register(registerationDTO).Result);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new AuthenticationResponseDTO {Errors=new List<string>() { "Invalid Authentication" } });
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthenticationResponseDTO { Success = true, Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> ExternalLogIn(ExternalLoginDTO externalLoginDTO) {
            var payload = await _jwtHandler.VerifyGoogleToken(externalLoginDTO);
            if (payload == null)
                return BadRequest("Invalid External Authentication.");
            var info = new UserLoginInfo(externalLoginDTO.Provider, payload.Subject, externalLoginDTO.Provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new ApplicationUser { Email = payload.Email,
                        UserName = payload.Email ,FirstName=payload.GivenName ,
                        LastName=payload.FamilyName,EmailConfirmed=payload.EmailVerified};
                    await _userManager.CreateAsync(user);
                    //prepare and send an email for the email confirmation
                    await _userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }
            if (user == null)
                return BadRequest("Invalid External Authentication.");
            //check for the Locked out account
            var claims = _jwtHandler.GetClaims(user);
            var token = _jwtHandler.GenerateToken(user,claims);
            return Ok(new AuthenticationResponseDTO { Token = token, Success = true });
        }
    }
}
