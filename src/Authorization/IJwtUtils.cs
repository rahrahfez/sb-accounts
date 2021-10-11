using sb_accounts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sb_accounts.Authorization
{
    public interface IJwtUtil
    {
        string GenerateJwtToken(Account account);
        public Guid? ValidateJwtToken(string token);
        RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
