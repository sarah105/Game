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

        public AuthenticationController(IMapper maper, IAuthService authService)
        {
            this.maper = maper;
            this.authService = authService;
        }
        //[Authorize] say that to access this fun you must have auth
        [HttpPost("register")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Register([FromBody] AccountDto account)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Account accountObj = maper.Map<Account>(account);
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
    }
}
