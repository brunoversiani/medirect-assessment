using MeDirectTest.Models;
using RestSharp;

namespace MeDirectTest.Service.Rates
{
    public interface IRateService
    {
        Task<RestResponse> IntegrationService(string clientId, RateRequestModel requestModel);
        Task<TransactionModel> LastRegisterService(string userId);
        Task<TransactionModel> NewExchangeWithOldRateService(TransactionModel model, double amount);
    }
}
