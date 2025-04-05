using Application.Dtos;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly CreateShortUrlUseCase _createShortUrlUseCase;

        public ShortUrlController(CreateShortUrlUseCase createShortUrlUseCase)
        {
            _createShortUrlUseCase = createShortUrlUseCase;
        }

        // GET: api/<ShortUrlController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ShortUrlController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShortUrlController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateShortUrlRequest createShortUrlRequest)
        {
            await _createShortUrlUseCase.ExecuteAsync(createShortUrlRequest);
            return Ok();
        }

        // PUT api/<ShortUrlController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShortUrlController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
