using MeDirectTest.Models;

namespace MeDirectTest.Service.Rates
{
    public interface IRateService
    {
        Task<TransactionModel> ConstructTransactionModel(string clientId, RateModel rateModel);
    }
}
