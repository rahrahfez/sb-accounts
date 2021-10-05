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

namespace sb_accounts.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _repository;
        private readonly IAccountService _service;
        private readonly IMapper _mapper;
        public AccountController(
            IAccountRepository repository,
            IAccountService service,
            IMapper mapper) 
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
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
                _repository.AddAccount(account);
                if (_repository.SaveChanges())
                {
                    var response = _mapper.Map<AccountResponseDTO>(account);
                    return CreatedAtRoute("Get", routeValues: new { id = account.Id }, value: response);
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
