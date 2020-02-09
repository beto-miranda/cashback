using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CashBack.Application.Common.Security;
using CashBack.Domain.Entities;
using CashBack.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CashBack.Web.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> logger;
        private readonly IResellerRepository resellerRepository;

        public AuthController(ILogger<AuthController> logger, IResellerRepository resellerRepository)
        {
            this.logger = logger;
            this.resellerRepository = resellerRepository;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid client request");
            }

            Reseller reseller = await resellerRepository.GetByEmail(loginModel.UserName);

            if (reseller == null || !PasswordHasher.VerifyHashedPassword(reseller.Password, loginModel.Password))
            {
                return Unauthorized();
            }
            
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Email, reseller.Email),
                    new Claim(ClaimTypes.Name, reseller.Name),
                    new Claim(ClaimTypes.NameIdentifier, reseller.Id.ToString("N"))
                },
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });
        }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}