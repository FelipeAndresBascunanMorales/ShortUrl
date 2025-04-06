using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateShortUrlRequest
    {
        public required string DteId { get; set; }
        public int? MaxUses { get; set; }

        // int or dateTime???
        public int? ExpirationDate { get; set; }

    }
}
