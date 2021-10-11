using sb_accounts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sb_accounts.Models
{
    public class AccountResponseDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int AvailableBalance { get; set; }
        public DateTime LastLogin { get; set; }
        public string JwtToken { get; set; }
        [JsonIgnore] // will be stored in http only cookie
        public string RefreshToken { get; set; }
        private AccountResponseDTO() { }
        public AccountResponseDTO(
            Guid id,
            string username, 
            int availableBalance, 
            DateTime lastLogin,
            string jwtToken,
            string refreshToken)
        {
            Id = id;
            Username = username;
            AvailableBalance = availableBalance;
            LastLogin = lastLogin;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
