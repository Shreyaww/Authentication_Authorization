using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Middleware_Auth.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Middleware_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous] // allows anyone to access this endpoint without authentication
        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if(user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User Not Found");
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            //sign the token using security key and sha256 algo
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claims are pieces of information(about the user) stored inside jwt 
            //here we are setting the values inside the claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role)
            };

            //define the token object

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(UserLogin userLogin)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(o => 
                                o.UserName.ToLower() == userLogin.UserName &&
                                o.Password == userLogin.Password);

            if(currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}
