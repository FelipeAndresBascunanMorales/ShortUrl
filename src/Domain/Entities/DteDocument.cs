using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DteDocument
    {
        public string Id { get; set; }
        public string DteType { get; set; } // 33 for Factura Electrónica
        public string RutEmisor { get; set; }
        public string RutReceptor { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaEmision { get; set; }
        public string EstadoSii { get; set; } // "ACEPTADO", "RECHAZADO", "REPARO"
    }
}
