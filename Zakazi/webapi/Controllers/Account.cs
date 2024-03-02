using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.Domain.DTOs;
using webapi.Domain.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        [HttpPost("register")] // api/account/register
        [ActionName("Register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
           // TO DO: implement register
            return Ok("User successfully registered");

        }

        [HttpPost("login")] // api/account/login 
        [ActionName("Login")]

        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            // TO DO: implement Log in with identity
            // return access token
            var accessToken = "";
            return Ok(accessToken);
        }

        [HttpPost("logout")] // api/account/logout  // Not Using it 
        [ActionName("Logout")]

        public async Task<ActionResult> Logout()
        {
            // TO DO: implement Log out with identity

            // not important if not using refresh tokens
            //Response.Cookies.Delete("refreshToken", new CookieOptions()
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.Lax,
            //});

            return Ok("User successfully logged out");
        }
    }
}
