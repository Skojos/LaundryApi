using System;
using LaundryWebApi.Dtos;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using LaundryWebApi.Extensions;

namespace LaundryWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
       

        public AuthController(IUserService userService)
        {
            _userService = userService;
          
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            return Created("Sucess", _userService.Create(dto));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
    
            var user = _userService.Authenticate(dto);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(new { user.Token});
        }

        
        [HttpGet("user")]
        public string GetUserData()
        {
           // int userId = int.Parse(HttpContext.GetUserId());

            return "Hej Jonas";


        }




    }
}
