using System;
using Xunit;
using sb_accounts.Entities;

namespace sb_accounts.tests.Entities
{
    public class AccountTest
    {
        [Fact]
        public void Account_AccountAvailableBalance()
        {
            var account = new Account(Guid.NewGuid(), "test", "test123") { AvailableBalance = 100 };

            Assert.False(account.HasSufficientBalance(101));
            Assert.True(account.HasSufficientBalance(99));
        }
        [Fact]
        public void Account_AccountUpdateUsername()
        {
            var account = new Account(Guid.NewGuid(), "test", "test123") { AvailableBalance = 100 };

            Assert.True(account.Username.Equals("test"));

            account.UpdateUsername("updated");

            Assert.True(account.Username.Equals("updated"));
        }
    }
}
