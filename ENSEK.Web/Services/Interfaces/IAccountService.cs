using ENSEK.Data.Access.Entities;
using ENSEK.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ENSEK.Web.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<MeterReading>> GetMeterReadings();

        Task<MeterReadingUploadResponse> UploadMeterReadingsCsvAsync(MeterReadingUploadRequest request);
    }
}
