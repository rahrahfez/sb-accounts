using System;
using Xunit;
using Moq;
using sb_accounts.Services;

namespace sb_accounts.tests.Services
{
    public class AuthenticationServiceTest
    {
        public AuthenticationService authenticationService = new();

        [Fact]
        public void AuthenticationService_CreatePasswordHash()
        {
            var passwordHash = authenticationService.CreatePasswordHash("password");

            Assert.NotNull(passwordHash);
        }
        [Fact]
        public void AuthenticationService_VerifyPasswordHash()
        {

            var passwordHash = authenticationService.CreatePasswordHash("password");

            Assert.True(authenticationService.VerifyPasswordHash(passwordHash, "password"));
            Assert.False(authenticationService.VerifyPasswordHash(passwordHash, "Password"));
        }
    }
}
