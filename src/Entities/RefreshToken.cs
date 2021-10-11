using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sb_accounts.Entities
{
    [Owned]
    public class RefreshToken
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedBy { get; set; }
        public string ReplacedByToken { get; set; }
        public bool IsRevoked => Revoked != null;
        public bool IsActive => !IsRevoked && !IsExpired;
        public bool IsExpired => DateTime.Now >= Expires;
    }
}
