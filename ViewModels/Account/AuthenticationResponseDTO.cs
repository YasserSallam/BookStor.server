using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Account
{
  public  class AuthenticationResponseDTO
    {
        public AuthenticationResponseDTO()
        {
            Errors = new List<string>();
        }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Token { get; set; }
    }
}
