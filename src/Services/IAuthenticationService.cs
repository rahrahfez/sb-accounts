using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sb_accounts.Services
{
    public interface IAuthenticationService
    {
        string CreatePasswordHash(string password);
        bool VerifyPasswordHash(string hashedPassword, string password);
    }
}
