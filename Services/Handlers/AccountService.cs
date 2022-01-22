using BookStore.Infrastructure.JwtFeatures;
using DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Models;
using Services.Contracts;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Handlers
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtHandler _jwtHandler;


        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<AuthenticationResponseDTO> Register(RegisterationDTO registerationDTO)
        {
            var respons = new AuthenticationResponseDTO();
            var user = new ApplicationUser()
            {
                FirstName = registerationDTO.FirstName,
                LastName = registerationDTO.LastName,
                Email = registerationDTO.Email,
                UserName = registerationDTO.Email.Split("@")[0]
            };
            var res = await _userManager.CreateAsync(user, registerationDTO.Password);
            if (res.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                respons.Success = true;
                respons.UserName = user.FirstName;

            }
            else
            {
                respons.Success = false;
                respons.Errors = new List<string>(res.Errors.Select(err => err.Description));

            }
            return respons;
        }

        public async Task<AuthenticationResponseDTO> Login(LoginDTO model)
        {
            var response = new AuthenticationResponseDTO();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                response.Success = false;
                response.Errors = new List<string>() { "Invalid Authentication" };
            }
            else
            {
                var signingCredentials = _jwtHandler.GetSigningCredentials();
                var claims = _jwtHandler.GetClaims(user);
                var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                response.Success = true;
                response.Token = token;

            }
            return response;
        }
        public async Task<AuthenticationResponseDTO> ExternalLogIn(ExternalLoginDTO externalLoginDTO) {
            var response = new AuthenticationResponseDTO();
            var payload = await _jwtHandler.VerifyGoogleToken(externalLoginDTO);
            if (payload == null)
            {
                response.Success = false;
                response.Errors.Add("Invalid External Authentication.");
            }
            else
            {
                var info = new UserLoginInfo(externalLoginDTO.Provider, payload.Subject, externalLoginDTO.Provider);
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(payload.Email);
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            Email = payload.Email,
                            UserName = payload.Email,
                            FirstName = payload.GivenName,
                            LastName = payload.FamilyName,
                            EmailConfirmed = payload.EmailVerified
                        };
                        await _userManager.CreateAsync(user);
                        //prepare and send an email for the email confirmation
                        await _userManager.AddLoginAsync(user, info);
                    }
                    else
                    {
                        await _userManager.AddLoginAsync(user, info);
                    }
                }
                
                var claims = _jwtHandler.GetClaims(user);
                var token = _jwtHandler.GenerateToken(user, claims);
                response.Success = true;
                response.Token = token;
                response.UserName = user.FirstName;
                response.ProfilePictureURL = payload.Picture;


            }
            return response;
        }
    }
}
