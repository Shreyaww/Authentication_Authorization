using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware_Auth.Models;
using System.Security.Claims;

namespace Middleware_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("Admin")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Admin()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hii {currentUser.GivenName}, you are a {currentUser.Role}");
        }

        [HttpGet("Seller")]
        [Authorize(Roles = "Seller")]

        //[Authorize(Roles = "Seller,Administrator")] to make it access to both the roles  
        public IActionResult Seller()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hii {currentUser.GivenName}, you are a {currentUser.Role}");
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value

                };
            }

            return null;
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hey Shreyaw");
        }




    }
}
