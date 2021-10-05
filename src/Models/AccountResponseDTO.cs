using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sb_accounts.Models
{
    public class AccountResponseDTO
    {
        public string Username { get; set; }
        public int AvailableBalance { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
