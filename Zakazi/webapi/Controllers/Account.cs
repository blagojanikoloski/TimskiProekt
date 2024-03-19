using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Domain.DTOs;
using webapi.Domain.Models;
using webapi.Service.Interface;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        //DI that are essential user management and also user sign in (test)
        private readonly UserManager<ZakaziUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public Account(UserManager<ZakaziUser> userManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }


        [HttpPost("register")] // api/account/register
        [ActionName("Register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            //test - create new identityuser with the email of the dto(register) 
            //attempt to create user in db through usermanager
            var uniqueCheck = await _userManager.FindByEmailAsync(registerDto.Email);
            if (uniqueCheck != null) return BadRequest("Email already taken");

            var user = _mapper.Map<ZakaziUser>(registerDto);
            user.Email = registerDto.Email;
            user.UserName = registerDto.Email;
            var result = await _userManager.CreateAsync(user,registerDto.Password);

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

            // check pass for the specific email from logindto
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var token = _tokenService.GenerateJwtToken(user);
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
    }



}
