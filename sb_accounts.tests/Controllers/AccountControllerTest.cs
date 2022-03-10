using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using sb_accounts.Repository;
using sb_accounts.Services;
using sb_accounts.Authorization;
using AutoMapper;

namespace sb_accounts.tests.Controllers
{
    public class AccountControllerTest
    { 
        public Mock<IAccountRepository> accountRepositoryMock = new();
        public Mock<IAuthenticationService> authenticationServiceMock = new();
        public Mock<IMapper> mapperMock = new();
        public Mock<IJwtUtil> jwtUtilMock = new();   

        [Fact]
        public void GetAllAccountsTest()
        {

        }
    }
}
