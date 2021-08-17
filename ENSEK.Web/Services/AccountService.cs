using ENSEK.Model;
using ENSEK.Web.Services.Interfaces;
using RestSharp;
using System.Net;
using System.Threading.Tasks;

namespace ENSEK.Web.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        private string _apiEndpointAddress;

        public AccountService(string apiEndpointAddress)
        {
            _apiEndpointAddress = apiEndpointAddress;
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

            // TODO deserialise response DTO e.g. JsonConvert.DeserializeObject<ModelDto>(response.Content);
            // TODO convert to local model with automapper e.g. _mapper.Map<Model>(ModelDto);

            return new MeterReadingUploadResponse
            {
            };
        }
    }
}
