using Microsoft.AspNetCore.Http;
using sb_accounts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sb_accounts.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAccountRepository repository, IJwtUtil jwtUtil)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var accountId = jwtUtil.ValidateJwtToken(token);
            if (accountId != null)
            {
                context.Items["Account"] = repository.GetAccountById(accountId.Value);
            }

            await _next(context);
        }
    }
}
