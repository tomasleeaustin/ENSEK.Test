using ENSEK.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ENSEK.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMeterReadingService _meterReadingService;

        public AccountController(ILogger<AccountController> logger, IMeterReadingService meterReadingService)
        {
            _logger = logger;
            _meterReadingService = meterReadingService;
        }

        [HttpPost]
        [Route("meter-reading-uploads")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MeterReadingUploads([FromQuery] IFormFile file)
        {
            // TODO: accept a csv
            //_meterReadingService.UploadMeterReadingsCsvAsync();

            if (false)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
