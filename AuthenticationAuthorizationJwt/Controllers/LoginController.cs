using AuthenticationAuthorizationJwt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authencate(userLogin);

            if(user != null)
            {
                var token =  Generate(user);
                return Ok(token);
            }

            return  NotFound("User not found");
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credetials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var clams = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role),

            };

            var token = new JwtSecurityToken
                (
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                clams,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credetials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authencate(UserLogin userLogin)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(x => x.UserName.ToLower() == 
            userLogin.UserName.ToLower() & x.Password == userLogin.Password);

            if(currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}
