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
            // Validate inputs
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }

            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null || !await _userService.VerifyPasswordAsync(user, password))
            {
                return new LoginResponse
                {
                    Autenticated = false,
                    Token = string.Empty,
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
