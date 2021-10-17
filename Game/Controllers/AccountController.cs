using AutoMapper;
using Game_DataAccess.Repositories;
using Game_Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Game.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly IMapper maper;

        public AccountController(IAccountRepository accountRepository, IMapper maper)
        {
            this.accountRepository = accountRepository;
            this.maper = maper;
        }

        [Authorize]
        [HttpGet("account")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public ActionResult GetAccount()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = identity.FindFirst("uid").Value;
                Account account = accountRepository.FindById(int.Parse(userId));
                if (account == null) return NotFound("Something went wrong!");
                return Ok(account);
            }
            return Unauthorized("Unauthorized!");
        }

        [Authorize]
        [HttpGet("update")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public ActionResult Update([FromBody] AccountDto accountDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = int.Parse(identity.FindFirst("uid").Value);

                Account account = accountRepository.FindById(userId);
                if (account == null) return NotFound("Something went wrong!");

                Account accountObj = maper.Map<Account>(account);
                accountObj.Id = userId;
                bool isUpdate = accountRepository.Update(accountObj);
                if (!isUpdate) return BadRequest("Something went wrong!");
                return Ok(accountObj);
            }
            return Unauthorized("Unauthorized!");
        }
        //[Authorize]
        [HttpGet("accounts")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            List<Account> accounts = accountRepository.List().ToList();
            if (accounts == null) return BadRequest("Something went wrong!");
            return Ok(accounts);
        }
    }
}
