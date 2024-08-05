using Microsoft.AspNetCore.Mvc;
using DependencyInjection.IServices;
using DependencyInjection.Services;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeServices _homeServices;
        public HomeController(IHomeServices homeServices)
        {
            _homeServices = homeServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHomeModels()
        {
            return Ok(await _homeServices.GetAllHomeModels());
        }
    }
}
