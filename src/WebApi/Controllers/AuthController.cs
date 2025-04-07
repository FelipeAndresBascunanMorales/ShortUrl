using Application.Dtos;
using Domain.Config;
using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IJwtService jwtService, JwtSettings jwtSettings)
        {
            _jwtService = jwtService;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
           // this is Homer Simpson eating a hotdog in the secret bunker of Mr Burns
            var user = AuthenticateUser(request.Username, request.Password);

            if (user == null)
                return Unauthorized();

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                Token = token,
                ExpiresIn = _jwtSettings.ExpirationMinutes * 60
            });
        }

        private User? AuthenticateUser(string username, string password)
        {
            // Replace with real user validation in production
            if (username == "admin" && password == "admin123")
            {
                return new User
                {
                    Id = "1",
                    Username = "admin",
                    Role = "Admin"
                };
            }

            if (username == "user" && password == "user123")
            {
                return new User
                {
                    Id = "2",
                    Username = "user",
                    Role = "User"
                };
            }

            return null;
        }
    }
}
