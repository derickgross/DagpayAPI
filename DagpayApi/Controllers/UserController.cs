using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DagpayApi.Models;
using DagpayApi.ViewModels;
using DagpayApi.Services;

namespace DagpayApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(AzureDatabaseContext context, IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserViewModel userViewModel)
        {
            var user = _userService.Authenticate(userViewModel.Username, userViewModel.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Incorrect username and password combination: Username " + userViewModel.Username + ", Password " + userViewModel.Password });
            }

            // Todo: Generate and return token
            var newToken = GenerateJWT(user);

            return Ok(new
            {
                token = newToken
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(UserViewModel userViewModel)
        {
            try
            {
                User user = _userService.Create(userViewModel);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private string GenerateJWT(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username)
                // add additional claims
            };
            Nullable<DateTime> expiration = DateTime.Now.AddDays(7);
            //string usernameClaim = new Claim(JwtRegisteredClaimNames.Sub, user.Username).ToString();
            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
