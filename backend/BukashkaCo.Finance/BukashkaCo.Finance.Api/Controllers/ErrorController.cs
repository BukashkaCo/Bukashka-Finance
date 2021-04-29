using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BukashkaCo.Finance.Api.Controllers
{
    [ApiController]
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("/error")]
        public async Task<IActionResult> Error()
        {
            return Problem();
        }
    }
}