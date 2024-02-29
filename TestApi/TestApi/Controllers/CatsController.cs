using Microsoft.AspNetCore.Mvc;
using TestApi.Models;
using TestApi.Services;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly ICatApiService _catService;
        public CatsController(ICatApiService catService)
        {
            _catService = catService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CatImage>>> GetCatImages(int numberToGet = 10)
        {
            var catImages = await _catService.GetCatImages(numberToGet);
            if (catImages != null)
            {
                return Ok(catImages);
            }
            else
            {
                return StatusCode(500, "Failed to fetch cat images");
            }
        }
    }
}
