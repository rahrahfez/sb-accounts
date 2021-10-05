using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using sb_accounts.Data;
using sb_accounts.Entities;

namespace sb_accounts.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _context;
        public AccountRepository(AccountContext context)
        {
            _context = context;
        }

        public void DeleteAccount(Account account)
        {
            _context.Remove(account);
        }

        public Account GetAccountById(Guid id)
        {
            return _context.Accounts.Find(id);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }
        public Account GetAccountByUsername(string username)
        {
            return _context.Accounts.FirstOrDefault(e => e.Username.Equals(username));
        }

        public void AddAccount(Account account)
        {
            _context.Add(account);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
