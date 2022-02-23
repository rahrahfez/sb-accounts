using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sb_accounts.Repository;

namespace sb_accounts.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public bool DoesUsernameExist(string username)
        {
            throw new NotImplementedException();
        }
    }
}
