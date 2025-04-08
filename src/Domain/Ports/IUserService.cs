using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IUserService
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> VerifyPasswordAsync(User user, string password);
    }
}
