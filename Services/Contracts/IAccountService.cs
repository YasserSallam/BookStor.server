using DTOs.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
   public interface IAccountService
    {
        Task<AuthenticationResponseDTO> Register(RegisterationDTO registerationDTO);
        Task<AuthenticationResponseDTO> Login(LoginDTO model);
        Task<AuthenticationResponseDTO> ExternalLogIn(ExternalLoginDTO externalLoginDTO);
    }
}
