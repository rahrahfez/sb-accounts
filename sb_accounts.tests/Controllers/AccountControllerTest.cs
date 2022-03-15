using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using sb_accounts.Repository;
using sb_accounts.Services;
using sb_accounts.Authorization;
using sb_accounts.Controllers;
using sb_accounts.Entities;
using sb_accounts.Models;
using AutoMapper;

namespace sb_accounts.tests.Controllers
{
    public class AccountControllerTest
    {
        private readonly AccountController controller;
        private readonly Mock<IAccountRepository> accountRepositoryMock;
        private readonly Mock<IAccountService> accountServiceMock;
        private readonly Mock<IAuthenticationService> authenticationServiceMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IJwtUtil> jwtUtilMock;   

        public AccountControllerTest()
        {
            accountRepositoryMock = new();
            accountServiceMock = new();
            authenticationServiceMock = new();
            mapperMock = new();
            jwtUtilMock = new();
            controller = new(
                accountRepositoryMock.Object,
                accountServiceMock.Object,
                authenticationServiceMock.Object,
                mapperMock.Object,
                jwtUtilMock.Object);
        }
        [Fact]
        public void AccountController_GetAllAccountsTest()
        {
            accountRepositoryMock.Setup(a => a.GetAllAccounts())
                .Returns(new List<Account>() { new Account(Guid.NewGuid(), "test", "password") });
            var result = controller.GetAllAccounts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var accounts = Assert.IsType<List<Account>>(okResult.Value);

            Assert.NotEmpty(accounts);

            var account = accounts.Find(a => a.Username == "test");

            Assert.Equal("test", account.Username);
        }
        [Fact]
        public void AccountController_GetAccountByIdTest_AccountFound()
        {
            var id = Guid.NewGuid();

            var account = new Account(id, "test", "");

            accountRepositoryMock.Setup(a => a.GetAccountById(It.IsAny<Guid>()))
                .Returns(account);

            var result = controller.GetAccountById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);

            mapperMock.Setup(m => m.Map<AccountResponseDTO>(It.IsAny<Account>()))
                .Returns(new AccountResponseDTO(id, "", 100, DateTime.Now, "", ""));

            var accRes = mapperMock.Object.Map<AccountResponseDTO>(okResult.Value);

            var acc = Assert.IsType<AccountResponseDTO>(accRes);

            Assert.Equal(id, acc.Id);
        }
        [Fact]
        public void AccountController_GetAccountByIdTest_AccountNotFound()
        {
            var id = Guid.NewGuid();

            accountRepositoryMock.Setup(a => a.GetAccountById(It.IsAny<Guid>()))
                .Returns(Account.NotFound);

            var result = controller.GetAccountById(id);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var acc = Assert.IsType<Account>(badRequestResult.Value);

            Assert.Equal(Guid.Empty, acc.Id);
        }
    }
}
