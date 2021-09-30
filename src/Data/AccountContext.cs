using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sb_accounts.Entities;

namespace sb_accounts.Data
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
    }
}
