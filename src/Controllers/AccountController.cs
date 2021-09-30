using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sb_accounts.Repository;

namespace sb_accounts.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _repository;
        public AccountController(IAccountRepository repository) 
        {
            _repository = repository;
        }
        [HttpPost("register")]
        public IActionResult RegisterNewAccount()
        {
            return Ok();
        }
    }
}
