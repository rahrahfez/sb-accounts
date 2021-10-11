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
        private readonly IAccountRepository _repository;
        private readonly IAccountService _service;
        private readonly IMapper _mapper;
        private readonly IJwtUtil _jwtUtil;
        public AccountController(
            IAccountRepository repository,
            IAccountService service,
            IMapper mapper,
            IJwtUtil jwtUtil) 
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
            _jwtUtil = jwtUtil;
        }
        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            var accounts = _repository.GetAllAccounts();
            return Ok(accounts);
        }
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetAccountById(Guid id)
        {
            var account = _repository.GetAccountById(id);
            if (account == null)
            {
                return BadRequest();
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
            if (_repository.GetAccountByUsername(accountRequestDTO.Username) != null)
            {
                return BadRequest("Username already exists.");
            }
            else
            {
                var hashedPassword = _service.CreatePasswordHash(accountRequestDTO.Password);
                var account = new Account(
                    accountRequestDTO.Username,
                    hashedPassword)
                {
                    AvailableBalance = 0
                };
                var jwtToken = _jwtUtil.GenerateJwtToken(account);
                var refreshToken = _jwtUtil.GenerateRefreshToken(ipAddress());
                var response = new AccountResponseDTO(
                  account.Id, 
                  accountRequestDTO.Username, 
                  account.AvailableBalance, 
                  account.CreatedAt, 
                  jwtToken, 
                  refreshToken.Token
                );

                account.RefreshTokens.Add(refreshToken);
                setTokenCookie(refreshToken.Token);
                _repository.AddAccount(account);
                _repository.SaveChanges();

                return CreatedAtRoute("Get", routeValues: new { id = account.Id }, value: response);
            }
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AccountRequestDTO accountRequestDTO)
        {
            var account = _repository.GetAccountByUsername(accountRequestDTO.Username);
            if (account != null && !_service.VerifyPasswordHash(account.PasswordHash, accountRequestDTO.Password))
                return BadRequest();

            var jwtToken = _jwtUtil.GenerateJwtToken(account);
            var refreshToken = _jwtUtil.GenerateRefreshToken(ipAddress());

            account.RefreshTokens.Add(refreshToken);
            account.LastLoginAt = DateTime.Now;
            setTokenCookie(refreshToken.Token);

            _repository.Update(account);
            _repository.SaveChanges();

            return Ok(new AccountResponseDTO(
              account.Id, 
              account.Username, 
              account.AvailableBalance, 
              account.LastLoginAt, 
              jwtToken, 
              refreshToken.Token
            ));
        }
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwared-For"))
                return Request.Headers["X-Forwared-For"];
            else return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        private void setTokenCookie(string token)
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
