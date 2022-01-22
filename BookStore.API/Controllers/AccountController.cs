using BookStore.Infrastructure.JwtFeatures;
using DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contracts;

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
        public ActionResult<AuthenticationResponseDTO> Login(LoginDTO model)
        {
           
            return Ok(_accountService.Login(model).Result);
        }

        [HttpPost]
        public  ActionResult<AuthenticationResponseDTO> ExternalLogIn(ExternalLoginDTO externalLoginDTO) {
            return Ok(_accountService.ExternalLogIn(externalLoginDTO).Result);
        }
    }
}
