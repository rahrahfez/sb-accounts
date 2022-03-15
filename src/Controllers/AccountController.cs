using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sb_accounts.Repository;
using sb_accounts.Models;
using sb_accounts.Entities;
using sb_accounts.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using sb_accounts.Authorization;

namespace sb_accounts.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountService _accountService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IJwtUtil _jwtUtil;
        public AccountController(
            IAccountRepository accountRepository,
            IAccountService accountService,
            IAuthenticationService authenticationService,
            IMapper mapper,
            IJwtUtil jwtUtil) 
        {
            _accountRepository = accountRepository;
            _accountService = accountService;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _jwtUtil = jwtUtil;
        }
        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            var accounts = _accountRepository.GetAllAccounts();
            return Ok(accounts);
        }
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetAccountById(Guid id)
        {
            var account = _accountRepository.GetAccountById(id);
            if (account == Account.NotFound)
            {
                // Returns Null Object if account is not found
                return BadRequest(account);
            }
            else
            {
                var accountResponse = _mapper.Map<AccountResponseDTO>(account);
                return Ok(accountResponse);
            }
        }
        [HttpPost("register")]
        public IActionResult RegisterNewAccount([FromBody]AccountRequestDTO accountRequestDTO)
        {
            if (_accountService.DoesUsernameExist(accountRequestDTO.Username) == true)
            {
                return BadRequest("Username already exists.");
            }
            else
            {
                var hashedPassword = _authenticationService.CreatePasswordHash(accountRequestDTO.Password);
                var account = new Account(Guid.NewGuid(), accountRequestDTO.Username, hashedPassword);
                var jwtToken = _jwtUtil.GenerateJwtToken(account);
                var refreshToken = _jwtUtil.GenerateRefreshToken(IpAddress());
                var response = new AccountResponseDTO(
                  account.Id, 
                  accountRequestDTO.Username, 
                  account.AvailableBalance, 
                  account.CreatedAt, 
                  jwtToken, 
                  refreshToken.Token
                );

                account.RefreshTokens.Add(refreshToken);
                SetTokenCookie(refreshToken.Token);
                _accountRepository.AddAccount(account);
                _accountRepository.SaveChanges();

                return CreatedAtRoute("Get", routeValues: new { id = account.Id }, value: response);
            }
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AccountRequestDTO accountRequestDTO)
        {
            var account = _accountRepository.GetAccountByUsername(accountRequestDTO.Username);
            if (account != null && !_authenticationService.VerifyPasswordHash(account.PasswordHash, accountRequestDTO.Password))
                return BadRequest();

            var jwtToken = _jwtUtil.GenerateJwtToken(account);
            var refreshToken = _jwtUtil.GenerateRefreshToken(IpAddress());

            account.RefreshTokens.Add(refreshToken);
            account.LastLoginAt = DateTime.Now;
            SetTokenCookie(refreshToken.Token);

            _accountRepository.Update(account);
            _accountRepository.SaveChanges();

            return Ok(new AccountResponseDTO(
              account.Id, 
              account.Username, 
              account.AvailableBalance, 
              account.LastLoginAt, 
              jwtToken, 
              refreshToken.Token
            ));
        }
        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwared-For"))
                return Request.Headers["X-Forwared-For"];
            else return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
