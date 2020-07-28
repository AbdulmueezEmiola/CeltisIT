using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CeltisIT.Model;
using CeltisIT.Model.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CeltisIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private CeltisITContext _context;
        
        public LoginController(IConfiguration config,CeltisITContext context)
        {
            _config = config;
            _context = context;            
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]UserDTO login)
        {
            IActionResult response = Unauthorized();
            User user = AuthenticateUser(login);
            if(user != null)
            {
                var tokenString = GenerateJwtToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }

        private object GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("fullname", user.FullName.ToString()),
                new Claim("role", user.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User AuthenticateUser(UserDTO login)
        {
            User user = _context.Users.SingleOrDefault(x => x.UserName == login.UserName);
            if(SecurePassword.ConfirmPassword(login.Password, user.Password))
            {
                return user;
            }
            return null;
        }
    }
}