using Application.Dtos;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class LoginUseCase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public LoginUseCase(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse> ExecuteAsync(string username, string password)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null || user.Password != password)
            {
                return new LoginResponse
                {
                    Autenticated = false,
                    Token = "",
                };
            }

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);
            // Set token expiration
            var expiration = DateTime.UtcNow.AddMinutes(30); // Example expiration time

            return new LoginResponse
            {
                Autenticated = true,
                Token = token,
                Expiration = expiration
            };
        }
    }
}
