using ENSEK.Api.Services.Interfaces;
using ENSEK.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        public async Task<ActionResult<MeterReadingUploadResponse>> MeterReadingUploads([FromBody] MeterReadingUploadRequest request)
        {
            MeterReadingUploadResponse response;

            try
            {
                response = await _meterReadingService.UploadMeterReadingsCsvAsync(request);
            }
            catch (Exception ex)
            {
                // TODO set up logger
                _logger.LogError(ex, ex.Message); 

                return BadRequest();
            }

            return response;
        }
    }
}
