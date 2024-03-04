using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Domain.DTOs;
using webapi.Domain.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        //DI that are essential user management and also user sign in (test)
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public Account(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        [HttpPost("register")] // api/account/register
        [ActionName("Register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            //test - create new identityuser with the name of the dto(register) 
            //attempt to create user in db through usermanager
            var user = new IdentityUser { UserName = registerDto.Name };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return Ok("User successfully registered");
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")] // api/account/login 
        [ActionName("Login")]

        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            // TO DO: implement Log in with identity
            // return access token
            
            // check pass for the specific username from logindto
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var token = GenerateJwtToken(user);
                return Ok(token);
            }
            return Unauthorized();
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

        private string GenerateJwtToken(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["TokenSettings:Issuer"],
                audience: _configuration["TokenSettings:Audience"],
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                },
                expires: DateTime.Now.AddMinutes(30), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }



}
