using Application.Dtos;
using Application.UseCases;
using Domain.Ports;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IWebHostEnvironment _env;

        public ShortUrlController(CreateShortUrlUseCase createShortUrlUseCase, RedirectShortUrlUseCase redirectShortUrlUseCase, IShortUrlRepository shortUrlRepository, IWebHostEnvironment env)
        {
            _createShortUrlUseCase = createShortUrlUseCase;
            _redirectShortUrlUseCase = redirectShortUrlUseCase;
            _shortUrlRepository = shortUrlRepository;
            _env = env;
        }


        // generate a new Short Code to see a DteDocument
        // POST api/<ShortUrlController>
        [HttpPost]
        [Authorize]
        [EndpointSummary("Create a new short URL")]
        [EndpointDescription("Create a new code that can be used to access a DteDocument")]
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

        //Once you have the code you are redirected to the DteDocument
        // GET api/<ShortUrlController>/5
        [Authorize]
        [HttpGet("{code}")]
        [EndpointSummary("Redirect to the original URL")]
        [EndpointDescription("Redirects to the original URL associated with the short code. If the short URL has expired or reached its max uses, an error message is returned.")]
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


        // GET: api/<ShortUrlController>
        [EndpointSummary("Get all short URLs")]
        [EndpointDescription("Retrieves all short URLs. This is for testing purposes only and is exposed only in development mode.")]
        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAsync()
        {
            //go out if the environment is not development
            if (!_env.IsDevelopment())
            {
                return BadRequest("This endpoint is only available in development mode.");
            }

            //get all short urls for testing only
            var shortUrls = await _shortUrlRepository.GetAllAsync();
            return Ok(shortUrls);
        }
    }
}
