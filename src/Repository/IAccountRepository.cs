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
        Account GetAccountByUsername(string username);
        void DeleteAccount(Account account);
        void AddAccount(Account account);
        void Update(Account account);
        IEnumerable<Account> GetAllAccounts();
    }
}
