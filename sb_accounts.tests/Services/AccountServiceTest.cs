using Moq;
using sb_accounts.Repository;
using sb_accounts.Entities;
using sb_accounts.Services;
using System;
using Xunit;

namespace sb_accounts.tests.Services
{
    public class AccountServiceTest
    {
        [Fact]
        public void AccountService_DoesUsernameExistTest()
        {
            var accountRepositoryMock = new Mock<IAccountRepository>();
            accountRepositoryMock.Setup(u => u.GetAccountByUsername("test")).Returns(new Account("test", "password"));

            var accountService = new AccountService(accountRepositoryMock.Object);
            var user = accountService.DoesUsernameExist("test");

            Assert.True(user);

            user = accountService.DoesUsernameExist("tst");

            Assert.False(user);
        }
    }
}
