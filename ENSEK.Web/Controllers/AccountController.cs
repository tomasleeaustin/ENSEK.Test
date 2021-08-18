using ENSEK.Model;
using ENSEK.Web.Models;
using ENSEK.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ENSEK.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IAccountService _accountService;

        public AccountController(
            ILogger<AccountController> logger,
            IAccountService accountService)
        {
            _logger = logger;

            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("account/meterreadings")]
        public async Task<IActionResult> MeterReadings()
        {
            var meterReadings = await _accountService.GetMeterReadings();

            return Json(new
            {
                meterReadings = meterReadings
            });
        }

        [HttpPost]
        [Route("account/uploadcsv")]
        public async Task<IActionResult> UploadCsv([FromBody] MeterReadingUploadRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CsvString))
            {
                return Json(new
                {
                    message = "No file detected",
                    success = false
                });
            }

            var response = await _accountService.UploadMeterReadingsCsvAsync(request);

            return Json(new
            {
                response = response,
                success = true
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
