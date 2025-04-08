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
                    new DteDocument { Id = "2", DteType = "33", RutEmisor = "76.123.456-7", RutReceptor = "12.345.678-9", MontoTotal = 50000, FechaEmision = DateTime.Now, EstadoSii = "ACEPTADO" },
                    new DteDocument { Id = "3", DteType = "33", RutEmisor = "76.123.456-7", RutReceptor = "98.765.432-1", MontoTotal = 75000, FechaEmision = DateTime.Now.AddDays(-2), EstadoSii = "RECHAZADO" },
                    new DteDocument { Id = "4", DteType = "33", RutEmisor = "76.123.456-7", RutReceptor = "87.654.321-0", MontoTotal = 120000, FechaEmision = DateTime.Now.AddDays(-3), EstadoSii = "ACEPTADO" },
                    new DteDocument { Id = "5", DteType = "33", RutEmisor = "76.123.456-7", RutReceptor = "76.543.210-9", MontoTotal = 30000, FechaEmision = DateTime.Now.AddDays(-4), EstadoSii = "PENDIENTE" },
                    new DteDocument { Id = "6", DteType = "33", RutEmisor = "76.123.456-7", RutReceptor = "65.432.109-8", MontoTotal = 95000, FechaEmision = DateTime.Now.AddDays(-5), EstadoSii = "ACEPTADO" },
                    new DteDocument { Id = "7", DteType = "33", RutEmisor = "76.123.456-7", RutReceptor = "54.321.098-7", MontoTotal = 110000, FechaEmision = DateTime.Now.AddDays(-6), EstadoSii = "RECHAZADO" },
                    new DteDocument { Id = "8", DteType = "33", RutEmisor = "76.123.456-7", RutReceptor = "43.210.987-6", MontoTotal = 60000, FechaEmision = DateTime.Now.AddDays(-7), EstadoSii = "ACEPTADO" },
                    new DteDocument { Id = "9", DteType = "33", RutEmisor = "76.123.456-7", RutReceptor = "32.109.876-5", MontoTotal = 85000, FechaEmision = DateTime.Now.AddDays(-8), EstadoSii = "PENDIENTE" },
                    new DteDocument { Id = "10", DteType = "33", RutEmisor = "76.123.456-7", RutReceptor = "21.098.765-4", MontoTotal = 40000, FechaEmision = DateTime.Now.AddDays(-9), EstadoSii = "ACEPTADO" }
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
