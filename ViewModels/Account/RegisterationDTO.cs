using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTOs.Account
{
  public  class RegisterationDTO
    {
        [Required(ErrorMessage ="First Name Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is require")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Password not match")]
        public string ConfirmPassword { get; set; }

    }
}
