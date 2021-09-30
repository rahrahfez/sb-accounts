using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sb_accounts.Entities;
using sb_accounts.Models;

namespace sb_accounts.Helpers
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<AccountRequestDTO, Account>();
            CreateMap<Account, AccountResponseDTO>();
        }
    }
}
