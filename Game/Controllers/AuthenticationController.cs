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
        private readonly IRepository<Account> accountRepository;
        private readonly IMapper maper;

        public AuthenticationController(IRepository <Account> accountRepository, IMapper maper)
        {
            this.accountRepository = accountRepository;
            this.maper = maper;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Register([FromBody] AccountDto account)
        {
            if (account == null) return BadRequest();
            Account accountObj = maper.Map<Account>(account);
            /*if (accountRepository.Find(account.Email) != null)
                return NotFound("Email Already Exist");
            if (accountRepository.Find(account.UserName) != null)
                return NotFound("User Name Already Exist");
            Account _account = accountRepository.Add(accountObj);
            _account.Password = "";*/
            return Ok(account);
        }
    }
}
