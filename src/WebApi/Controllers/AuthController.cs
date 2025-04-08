using Application.Dtos;
using Application.UseCases;
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
        private readonly LoginUseCase _loginUseCase;

        public AuthController(LoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var response = _loginUseCase.ExecuteAsync(request.Username, request.Password).Result;
            if (response.Autenticated)
            {
                return Ok(response);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
