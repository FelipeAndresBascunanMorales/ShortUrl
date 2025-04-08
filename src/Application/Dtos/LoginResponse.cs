using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class LoginResponse
    {
        public bool Autenticated { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }

    }
}
