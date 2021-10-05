using System;
using Xunit;
using sb_accounts.Entities;

namespace sb_accounts.tests
{
    public class AccountTest
    {
        [Fact]
        public void AccountAvailableBalance()
        {
            var account = new Account("test", "test123") { AvailableBalance = 100 };

            Assert.False(account.HasSufficientBalance(101));
            Assert.True(account.HasSufficientBalance(99));
        }
    }
}
