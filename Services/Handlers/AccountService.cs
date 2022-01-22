using DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Handlers
{
  public  class AccountService: IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResponseDTO> Register(RegisterationDTO registerationDTO) {
            var respons = new AuthenticationResponseDTO();
            var user = new ApplicationUser() {
                FirstName=registerationDTO.FirstName,
                LastName=registerationDTO.LastName,
                Email = registerationDTO.Email,
            UserName=registerationDTO.Email.Split("@")[0]};
        var res=await _userManager.CreateAsync(user, registerationDTO.Password);
            if (res.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                respons.Success = true;
            }
            else { 
                respons.Success = false;
                respons.Errors = new List<string>(res.Errors.Select(err=>err.Description));

            }
            return respons;
        }

        }
}
