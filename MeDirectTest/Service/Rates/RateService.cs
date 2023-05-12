using Azure;
using MeDirectTest.Data;
using MeDirectTest.Data.Repository.Rates;
using MeDirectTest.Models;
using MeDirectTest.Service.User;
using Newtonsoft.Json;
using RestSharp;

namespace MeDirectTest.Service.Rates
{
    public class RateService : IRateService
    {
        private readonly IUserService _userService;
        private readonly IRateRepository _rateRepository;

        public RateService(IUserService userService, IRateRepository rateRepository)
        {
            _userService = userService;
            _rateRepository = rateRepository;
        }

        public async Task<RestResponse> IntegrationService(string clientid, RateRequestModel requestModel)
        {
            var client = new RestClient($"https://api.apilayer.com/exchangerates_data/convert?to={requestModel.To}&from={requestModel.From}&amount={requestModel.Amount}");
            
            var request = new RestRequest("", Method.Get);
            request.AddHeader("apikey", "6JTABFgsuQ1LK0DTmRyMg6xYd1SYmM1s");

            RestResponse response = client.Execute(request);
            await AddTransactionService(clientid, response);
            
            return response;
        }

        public async Task<TransactionModel> AddTransactionService(string clientId, RestResponse response)
        {
            RateResponseModel responseContent = JsonConvert.DeserializeObject<RateResponseModel>(response.Content);

            var rateResponseModel = ConstructRateResponseModel(responseContent);
            TransactionModel transactionModel = await ConstructTransactionModel(clientId, rateResponseModel);

            return await _rateRepository.AddTransactionRep(transactionModel);
        }

        public async Task<TransactionModel> LastRegisterService (string userId)
        {
            TransactionModel lastRegister = await _rateRepository.LastTransactionPerUserRep(userId);

            return lastRegister;
        }

        public async Task<TransactionModel> NewExchangeWithOldRateService(TransactionModel model, double amount)
        {
            TransactionModel updatedModel = UpdateOldRateTransactionModel(model, amount);
            return await _rateRepository.AddTransactionRep(updatedModel);
        }

        #region Private Model Constructors
        private async Task<TransactionModel> ConstructTransactionModel(string clientId, RateResponseModel rateResponseModel)
        {
            UserModel userModel = await _userService.SearchByUserIdService(clientId);
            DateTime dateStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            TransactionModel model = new TransactionModel()
            {
                TransactionId = Guid.NewGuid().ToString(),
                TrClientId = userModel.ClientId,
                TrFirstName = userModel.FirstName,
                TrLastName = userModel.LastName,
                TrFromCurrency = rateResponseModel.Query.From,
                TrFromAmount = rateResponseModel.Query.Amount,
                TrToCurrency = rateResponseModel.Query.To,
                TrRate = rateResponseModel.Info.Rate,
                TrResult = rateResponseModel.Result,
                TrRateTimestamp = dateStart.AddSeconds(rateResponseModel.Info.Timestamp),
                TransactionTimestamp = DateTime.UtcNow
            };
            return model;
        }

        private TransactionModel UpdateOldRateTransactionModel(TransactionModel transactionModel, double amount)
        {
            transactionModel = new TransactionModel()
            {
                TransactionId = Guid.NewGuid().ToString(),
                TrClientId = transactionModel.TrClientId,
                TrFirstName = transactionModel.TrFirstName,
                TrLastName = transactionModel.TrLastName,
                TrFromCurrency = transactionModel.TrFromCurrency,
                TrFromAmount = amount,
                TrToCurrency = transactionModel.TrToCurrency,
                TrRate = transactionModel.TrRate,
                TrResult = transactionModel.TrRate * amount,
                TrRateTimestamp = transactionModel.TrRateTimestamp,  //old rate timestamp
                TransactionTimestamp = DateTime.Now //new transaction
            };
            return transactionModel;
        }

        private RateResponseModel ConstructRateResponseModel(RateResponseModel response)
        {
            response = new RateResponseModel()
            {
                Date = response.Date,
                Historical = response.Historical,
                Result = response.Result,
                Success = response.Success,
                Info = new Info()
                {
                    Rate = response.Info.Rate,
                    Timestamp = response.Info.Timestamp,
                },
                Query = new Query()
                {
                    From = response.Query.From,
                    To = response.Query.To,
                    Amount = response.Query.Amount,
                },
            };
            return response;
        }
        #endregion
    }
}
