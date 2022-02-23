using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scrypt;

namespace sb_accounts.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public string CreatePasswordHash(string password)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            string hashedPassword = encoder.Encode(password);
            return hashedPassword;
        }

        public bool VerifyPasswordHash(string hashedPassword, string password)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            return encoder.Compare(password, hashedPassword);
        }
    }
}
