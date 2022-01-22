using Microsoft.AspNetCore.Identity;
using Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Handlers
{
   public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepository(UserManager<ApplicationUser> _userManager)
        {

        }
        void Register(ApplicationUser user) {
            _userManager.CreateAsync(user, user.PasswordHash);
        }
    }
}
