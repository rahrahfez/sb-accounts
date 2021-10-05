using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sb_accounts.Entities
{
    [Table("accounts")]
    public class Account
    {
        [Key]
        [Column("id")]
        public Guid Id { get; private set; }
        [Column("username")]
        public string Username { get; private set; }
        [Column("password_hash")]
        public string HashedPassword { get; private set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("last_login")]
        public DateTime LastLoginAt { get; set; }
        [Column("available_balance")]
        public int AvailableBalance { get; set; }
        [Column("refresh_token")]
        public string RefreshToken { get; set; }
        [Column("refresh_token_expiry")]
        public DateTime RefreshTokenExpiry { get; set; }
        private Account() { }
        public Account(
            string username,
            string hashedPassword)
        {
            Id = Guid.NewGuid();
            Username = username;
            HashedPassword = hashedPassword;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            LastLoginAt = DateTime.Now;
        }
        public void UpdateUsername(Account account, string username)
        {
            account.Username = username;
        }
        public bool HasSufficientBalance(int wagerAmount)
        {
            if (wagerAmount > AvailableBalance)
                return false;
            else return true;
        }
    }
}
