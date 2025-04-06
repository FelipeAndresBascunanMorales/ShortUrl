using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IDteDocumentRepository
    {
        public Task<bool> ExistAsync(string dteId);

        public Task<DteDocument?> GetByIdAsync(string dteId);
    }
}
