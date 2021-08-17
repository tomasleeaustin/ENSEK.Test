using ENSEK.Api.Models;
using System.Threading.Tasks;

namespace ENSEK.Api.Services.Interfaces
{
    public interface IMeterReadingService
    {
        Task<MeterReadingUploadResponse> UploadMeterReadingsCsvAsync(MeterReadingUploadRequest request);
    }
}
