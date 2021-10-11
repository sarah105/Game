using AutoMapper;
using Game_DataAccess.Repositories;
using Game_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Controllers
{
    [Route("api/v{version:apiVersion}/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly IMapper maper;

        public AuthenticationController(IAccountRepository accountRepository, IMapper maper)
        {
            this.accountRepository = accountRepository;
            this.maper = maper;
        }
        //[Authorize] say that to access this fun you must have auth
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Register([FromBody] AccountDto account)
        {
            if (account == null) return BadRequest();
            Account accountObj = maper.Map<Account>(account);
            if (accountRepository.Find(account.Email) != null)
                return NotFound("Email Already Exist");
            /*if (accountRepository.Find(account.UserName) != null)
                return NotFound("User Name Already Exist");*/
            Account _account = accountRepository.Add(accountObj);
            _account.Password = "";
            return Ok(_account);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            List <Account> accounts = accountRepository.List().ToList();
            return Ok(accounts);
        }
    }
}
