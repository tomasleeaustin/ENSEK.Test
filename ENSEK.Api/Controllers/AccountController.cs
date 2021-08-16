using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENSEK.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("meter-reading-uploads")]
        public IActionResult MeterReadingUploads()
        {
            return Ok();
        }
    }
}
