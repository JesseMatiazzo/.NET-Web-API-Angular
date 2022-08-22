using System;
using System.Collections.Generic;
using System.Text;

namespace ProEventos.Domain.Response
{
    public class UserResponse
    {
        public string UserName { get; set; }
        public string PrimeiroNome { get; set; }
        public string Token { get; set; }

        public UserResponse(string userName, string primeiroNome, string token)
        {
            UserName = userName;
            PrimeiroNome = primeiroNome;
            Token = token;
        }
    }
}
