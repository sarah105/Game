using Game.Helpers;
using Game_DataAccess.Repositories;
using Game_Models.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Game.Services
{
    public class AuthService : IAuthService
    {
        private readonly JWT jwt;
        private readonly IAccountRepository accountRepository;
        private readonly EncryptDecrypt encryptPassword ;

        public AuthService(IOptions<JWT> jwt, IAccountRepository accountRepository)
        {
            this.jwt = jwt.Value;
            this.accountRepository = accountRepository;
            this.encryptPassword = new EncryptDecrypt();
        }

        public Auth Register(Account account)
        {
            if (accountRepository.FindByEmail(account.Email) != null)
            {
                return new Auth { Message = "Email is already registered!" };
            }
            if (accountRepository.FindByUsername(account.UserName) != null)
            {
                return new Auth { Message = "Username is already registered!" };
            }
            account.Password = encryptPassword.Encrypt(account.Password);
            Account _account = accountRepository.Add(account);
            if (account == null) return new Auth { Message = "Something went wrong!" };
            _account.Password = "";

            var jwtSecurityToken = CreateJwtToken(_account.Id);

            Auth auth =  new Auth
            {
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Account = _account,
                ExpiresOn = jwtSecurityToken.ValidTo
            };
            return auth;
        }

        public Auth Login(LoginModelDto account)
        {
            Account searchRes = accountRepository.FindByEmail(account.Email);
            bool checkPassword = searchRes != null ? encryptPassword.CheckPassword(searchRes.Password, account.Password) : false;
            if (searchRes == null || !checkPassword)
            {
                return new Auth { Message = "Email or password is wrong" };
            }
            var jwtSecurityToken = CreateJwtToken(searchRes.Id);
            searchRes.Password = "";
            Auth auth = new Auth
            {
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Account = searchRes,
                ExpiresOn = jwtSecurityToken.ValidTo
            };
            return auth;
        }

        private JwtSecurityToken CreateJwtToken(int id)
        {
            var claims = new[]
            {
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", id.ToString())
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
