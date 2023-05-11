using MeDirectTest.Models;
using RestSharp;

namespace MeDirectTest.Service.Rates
{
    public interface IRateService
    {
        Task<RestResponse> IntegrationService(string clientId, RateRequestModel requestModel);
        Task<TransactionModel> ConstructTransactionModel(string clientId, RateResponseModel rateModel);
    }
}
