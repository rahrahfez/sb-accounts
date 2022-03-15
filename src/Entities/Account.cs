using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
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
        public string PasswordHash { get; private set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("last_login")]
        public DateTime LastLoginAt { get; set; }
        [Column("available_balance")]
        public int AvailableBalance { get; set; }
        [Column("refresh_tokens")]
        public List<RefreshToken> RefreshTokens { get; set; }
        public Account(
            Guid id,
            string username,
            string hashedPassword,
            int availableBalance = 0)
        {
            Id = id;
            Username = username;
            PasswordHash = hashedPassword;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            LastLoginAt = DateTime.Now;
            AvailableBalance = availableBalance;
        }
        public void UpdateUsername(string username)
        {
            Username = username;
        }
        public bool HasSufficientBalance(int wagerAmount)
        {
            if (wagerAmount > AvailableBalance) return false;
            else return true;
        }
        public static readonly Account NotFound = new(Guid.Empty, "", "");
    }
}
