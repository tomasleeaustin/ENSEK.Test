using CsvHelper;
using ENSEK.Api.Models;
using ENSEK.Api.Services.Interfaces;
using ENSEK.Data.Access.DbContexts.Interfaces;
using ENSEK.Model;
using System;
using System.Globalization;
using System.IO;
using System.Text;
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
            // TODO: potentially receive csv as a file, though that would mean storing it somewhere first.

            if (string.IsNullOrWhiteSpace(request.CsvString))
            {
                throw new Exception("No csv content found");
            }

            // TODO: parse csv (maybe add nuget CSVHelper https://joshclose.github.io/CsvHelper/examples/reading/get-class-records/)

            var byteArray = Encoding.UTF8.GetBytes(request.CsvString);

            using (var stream = new MemoryStream(byteArray))
            using (var streamReader = new StreamReader(stream))
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<MeterReadingDto>();

                var i = 0;
            }



            // TODO: validate (count success/fail)
            // TODO: insert success
            // TODO: response should contain number of success and fail

            throw new NotImplementedException();
        }
    }
}
