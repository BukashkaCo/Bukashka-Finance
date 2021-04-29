using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BukashkaCo.Finance.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class TestController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Content("Content");
        }
    }
}