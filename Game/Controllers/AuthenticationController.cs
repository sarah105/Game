using AutoMapper;
using Game.Services;
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
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/v{version:apiVersion}")]

    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper maper;
        private readonly IAuthService authService;
        private readonly IAccountRepository accountRepository;

        public AuthenticationController(IMapper maper, IAuthService authService, IAccountRepository accountRepository)
        {
            this.maper = maper;
            this.authService = authService;
            this.accountRepository = accountRepository;
        }
        //[Authorize] say that to access this fun you must have auth
        [HttpPost("register")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Register([FromBody] AccountDto account)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Account accountObj = maper.Map<Account>(account);
            //accountObj.Password = BCrypt.Net.BCrypt.HashPassword()
            Auth auth = authService.Register(accountObj);
            if (!auth.IsAuthenticated)
            {
                return NotFound(auth.Message);
            }
            return Ok(new { auth.Token, auth.Account });
        }

        [HttpPost("login")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Login([FromBody] LoginModelDto loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Auth auth = authService.Login(loginModel);
            if(!auth.IsAuthenticated) return NotFound(auth.Message);

            return Ok(new { auth.Token, auth.Account });
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
                // or
                var userId = identity.FindFirst("uid").Value;
                Account account = accountRepository.Find(int.Parse(userId));
                if (account == null) return NotFound("Something went wrong!");
                return Ok(account);
            }
            return Unauthorized("Unauthorized!");
        }

        //[Authorize]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            List <Account> accounts = accountRepository.List().ToList();
            if(accounts == null) return BadRequest("Something went wrong!");
            return Ok(accounts);
        }
    }
}
