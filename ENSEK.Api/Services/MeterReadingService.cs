using ENSEK.Api.Models;
using ENSEK.Api.Services.Interfaces;
using ENSEK.Data.Access.DbContexts.Interfaces;
using System;
using System.Threading.Tasks;

namespace ENSEK.Api.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly IEnsekDbContext _ensekDbContext;

        public MeterReadingService(IEnsekDbContext ensekDbContext)
        {
            _ensekDbContext = ensekDbContext;
        }

        public Task<MeterReadingUploadResponse> UploadMeterReadingsCsvAsync(MeterReadingUploadRequest request)
        {
            // TODO: parse csv (maybe add nuget CSVHelper https://joshclose.github.io/CsvHelper/examples/reading/get-class-records/)
            // TODO: validate (count success/fail)
            // TODO: insert success
            // TODO: response should contain number of success and fail

            throw new NotImplementedException();
        }
    }
}
