using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MockDteDocumentRepository : IDteDocumentRepository
    {
        private readonly List<DteDocument> _dteDocuments = new List<DteDocument>
                {
                    new DteDocument { Id = "1", DteType = "33", RutEmisor = "76.123.456-7", RutReceptor = "65.987.654-3", MontoTotal = 100000, FechaEmision = DateTime.Now.AddDays(-1), EstadoSii = "ACEPTADO" },
                    new DteDocument { Id = "2", DteType = "33", RutEmisor = "76.123.456-7", RutReceptor = "12.345.678-9", MontoTotal = 50000, FechaEmision = DateTime.Now, EstadoSii = "ACEPTADO" }
                };

        public Task<bool> ExistAsync(string dteId)
        {
            return Task.FromResult(_dteDocuments.Any(d => d.Id == dteId));
        }

        public Task<DteDocument?> GetByIdAsync(string dteId)
        {
            return Task.FromResult(_dteDocuments.FirstOrDefault(d => d.Id == dteId));
        }
    }
}
