using Domain.Entities;
using Domain.Ports;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace WebApi.Controllers
{
    public class DteDocumentController : Controller
    {
        private readonly IDteDocumentRepository mockSiiService;
        public DteDocumentController(IDteDocumentRepository mockSiiService)
        {
            this.mockSiiService = mockSiiService;
        }

        [HttpGet("dte/{code}")]
        public async Task<IActionResult> GetDteDocumentsAsXml(string code)
        {
            var dteDocument = await mockSiiService.GetByIdAsync(code);

            if(dteDocument == null)
            {
                return NotFound(new { Error = "DTE document not found" });
            }

            var xmlSerializer = new XmlSerializer(typeof(DteDocument));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, dteDocument);
                return Content(stringWriter.ToString(), "application/xml");
            }
        }
    }
}
