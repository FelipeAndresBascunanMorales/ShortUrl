using Application.Dtos;
using Application.UseCases;
using Domain.Ports;
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
        private readonly RedirectShortUrlUseCase _redirectShortUrlUseCase;
        private readonly IShortUrlRepository _shortUrlRepository;

        public ShortUrlController(CreateShortUrlUseCase createShortUrlUseCase, RedirectShortUrlUseCase redirectShortUrlUseCase, IShortUrlRepository shortUrlRepository)
        {
            _createShortUrlUseCase = createShortUrlUseCase;
            _redirectShortUrlUseCase = redirectShortUrlUseCase;
            _shortUrlRepository=shortUrlRepository;
        }

        // GET: api/<ShortUrlController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            //get all short urls for testing only
            var shortUrls = await _shortUrlRepository.GetAllAsync();
            return Ok(shortUrls);
        }

        //Once you have the code you are redirected to the DteDocument
        // GET api/<ShortUrlController>/5
        [HttpGet("{code}")]
        public async Task<IActionResult> GetAsync(string code)
        {
            //redirect to the original URL
            try
            {
                var result = await _redirectShortUrlUseCase.ExecuteAsync(code);

                if (result.Expired)
                    return BadRequest("Short URL has expired or reached max uses");

                return Redirect(result.Url);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
            
        // generate a new Short Code to see a DteDocument
        // POST api/<ShortUrlController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateShortUrlRequest createShortUrlRequest)
        {
            try
            {
                var shortUrl = await _createShortUrlUseCase.ExecuteAsync(createShortUrlRequest);
                return Ok(new { Code = shortUrl.EncodedUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
