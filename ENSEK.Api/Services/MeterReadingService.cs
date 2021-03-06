using AutoMapper;
using CsvHelper;
using ENSEK.Api.Models;
using ENSEK.Api.Services.Interfaces;
using ENSEK.Api.Validators;
using ENSEK.Data.Access.DbContexts.Interfaces;
using ENSEK.Data.Access.Entities;
using ENSEK.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Api.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly IEnsekDbContext _ensekDbContext;
        private readonly IMapper _mapper;

        public MeterReadingService(IEnsekDbContext ensekDbContext, IMapper mapper)
        {
            _ensekDbContext = ensekDbContext;
            _mapper = mapper;
        }

        public async Task<MeterReadingUploadResponse> UploadMeterReadingsCsvAsync(MeterReadingUploadRequest request)
        {
            var response = new MeterReadingUploadResponse();

            if (string.IsNullOrWhiteSpace(request.CsvString))
            {
                throw new Exception("No CSV content found.");
            }

            IEnumerable<MeterReadingDto> csvRecords = null;
            var byteArray = Encoding.UTF8.GetBytes(request.CsvString);

            using (var stream = new MemoryStream(byteArray))
            using (var streamReader = new StreamReader(stream))
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                csvRecords = csvReader.GetRecords<MeterReadingDto>().ToList();
            }

            if (csvRecords == null || csvRecords.Count() == 0)
            {
                throw new Exception("Could not parse CSV records.");
            }

            var recordsToAdd = await ValidateCsvRecords(csvRecords, response);

            SaveMeterReadings(recordsToAdd);

            return response;
        }

        private void SaveMeterReadings(IEnumerable<MeterReading> recordsToAdd)
        {
            try
            {
                _ensekDbContext.MeterReadings.AddRange(recordsToAdd);
                _ensekDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<IEnumerable<MeterReading>> ValidateCsvRecords(
            IEnumerable<MeterReadingDto> csvRecords, 
            MeterReadingUploadResponse response)
        {
            var recordsToAdd = new List<MeterReading>();
            var validator = new MeterReadingValidator();

            foreach (var record in csvRecords)
            {
                // Validate data formats (spec "Reading values should be in the format NNNNN")
                var result = validator.Validate(record);

                if (!result.IsValid)
                {
                    response.FailCount++;

                    continue;
                }

                // Map the record to an entity.
                var meterReading = _mapper.Map<MeterReading>(record);

                // Verify account exists (spec "A meter reading must be associated with an
                // Account ID to be deemed valid")
                var account = await _ensekDbContext.Accounts
                    .AsQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(account => account.Id == meterReading.AccountId);

                if (account == null)
                {
                    response.FailCount++;

                    continue;
                }

                // Check for duplicate records from the CSV or in the existing data (spec "You should not be able to load the same entry twice")
                var matchingRecords = recordsToAdd.Where(r => r.AccountId == meterReading.AccountId && r.Value == meterReading.Value);
                var matchingDbReadings = await _ensekDbContext.MeterReadings
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(mr => mr.AccountId == account.Id && mr.Value == meterReading.Value)
                    .ToListAsync();

                if (matchingRecords.Count() > 0 || matchingDbReadings.Count() > 0)
                {
                    response.FailCount++;

                    continue;
                }

                recordsToAdd.Add(meterReading);
                response.SuccessCount++;
            }

            return recordsToAdd;
        }
    }
}
