using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sb_accounts.Entities;

namespace sb_accounts.Repository
{
    public interface IAccountRepository
    {
        bool SaveChanges();
        Account GetAccountById(Guid id);
        void DeleteAccount(Account account);
        IEnumerable<Account> GetAllAccounts();
    }
}
