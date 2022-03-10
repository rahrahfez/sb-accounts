using Moq;
using sb_accounts.Repository;
using System;
using Xunit;
namespace sb_accounts.tests.Services
{
    public class AccountServiceTest
    {
        public Mock<IAccountRepository> accountRepositoryMock = new();

        [Fact]
        public void AccountService_DoesUsernameExistTest()
        {

        }
    }
}
