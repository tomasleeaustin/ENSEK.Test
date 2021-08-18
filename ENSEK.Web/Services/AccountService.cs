using ENSEK.Data.Access.DbContexts.Interfaces;
using ENSEK.Data.Access.Entities;
using ENSEK.Model;
using ENSEK.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ENSEK.Web.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        private readonly IEnsekDbContext _ensekDbContext;

        private string _apiEndpointAddress;

        public AccountService(IEnsekDbContext ensekDbContext, string apiEndpointAddress)
        {
            _ensekDbContext = ensekDbContext;
            _apiEndpointAddress = apiEndpointAddress;
        }

        public async Task<IEnumerable<MeterReading>> GetMeterReadings()
        {
            List<MeterReading> meterReadings = null;

            try
            {
                meterReadings = await _ensekDbContext.MeterReadings
                    .AsQueryable()
                    .AsNoTracking()
                    .OrderBy(mr => mr.AccountId)
                        .ThenBy(mr => mr.DateTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return meterReadings;
        }

        public async Task<MeterReadingUploadResponse> UploadMeterReadingsCsvAsync(MeterReadingUploadRequest request)
        {
            var client = GetRestClient(_apiEndpointAddress);

            var restRequest = new RestRequest("account/meter-reading-uploads", Method.POST);
            restRequest.AddJsonBody(request);

            var restResponse = await client.ExecuteAsync(restRequest);

            if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                // Error
            }

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                // Unknown error
            }

            var response = JsonConvert.DeserializeObject<MeterReadingUploadResponse>(restResponse.Content);

            return response;
        }
    }
}
