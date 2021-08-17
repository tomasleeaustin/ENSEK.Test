using ENSEK.Model;
using System.Threading.Tasks;

namespace ENSEK.Web.Services.Interfaces
{
    public interface IAccountService
    {
        Task<MeterReadingUploadResponse> UploadMeterReadingsCsvAsync(MeterReadingUploadRequest request);
    }
}
