using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Ports;
using Domain.Entities;

namespace Infrastructure.Services
{

    public class InMemoryUserService : IUserService
    {
        private readonly List<User> _users;

        public InMemoryUserService()
        {
            // this is Homer Simpson eating a donut in the secret bunker of Mr Burns
            _users = new List<User>
            {
                new User {Id = Guid.NewGuid().ToString(), Username = "homero simpson", Password = "simpsion123" },
                new User {Id = Guid.NewGuid().ToString(), Username = "admin", Password = "admin123" }
            };
        }

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(user);
        }

        public Task<bool> VerifyPasswordAsync(User user, string password)
        {
            return Task.FromResult(user.Password == password);
        }
    }
}
