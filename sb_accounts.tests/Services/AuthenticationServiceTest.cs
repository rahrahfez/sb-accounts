using System;
using Xunit;
using sb_accounts.Services;

namespace sb_accounts.tests.Services
{
    public class AuthenticationServiceTest
    {
        [Fact]
        public void AuthenticationService_CreatePasswordHash()
        {
            AuthenticationService authenticationService = new AuthenticationService();

            var passwordHash = authenticationService.CreatePasswordHash("password");

            Assert.NotNull(passwordHash);
        }
        [Fact]
        public void AuthenticationService_VerifyPasswordHash()
        {
            AuthenticationService authenticationService = new AuthenticationService();

            var passwordHash = authenticationService.CreatePasswordHash("password");

            Assert.True(authenticationService.VerifyPasswordHash(passwordHash, "password"));
            Assert.False(authenticationService.VerifyPasswordHash(passwordHash, "Password"));
        }
    }
}
