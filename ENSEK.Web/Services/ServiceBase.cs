using RestSharp;

namespace ENSEK.Web.Services
{
    public abstract class ServiceBase
    {
        protected RestClient GetRestClient(string endpointAddress)
        {
            var client = new RestClient(endpointAddress);

            //TODO Add some form of authentication, e.g. an API Key.
            //client.AddDefaultHeader("Api-Key", "123456789");

            return client;
        }
    }
}
