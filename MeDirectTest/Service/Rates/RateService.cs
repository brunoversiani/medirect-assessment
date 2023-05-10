using MeDirectTest.Models;
using MeDirectTest.Repository;
using MeDirectTest.Service.User;

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

        public async Task<TransactionModel> ConstructTransactionModel(string clientId, RateModel rateModel)
        {
            UserModel userModel = await _userService.SearchByUserIdService(clientId);
            DateTime dateStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            TransactionModel model = new TransactionModel()
            {
                TransactionId = Guid.NewGuid().ToString(),
                TrClientId = userModel.ClientId,
                TrFirstName = userModel.FirstName,
                TrLastName = userModel.LastName,
                TrFromCurrency = rateModel.Query.From,
                TrFromAmount = rateModel.Query.Amount,
                TrToCurrency = rateModel.Query.To,
                TrRate = rateModel.Info.Rate,
                TrResult = rateModel.Result,
                TrRateTimestamp = dateStart.AddSeconds(rateModel.Info.Timestamp),
                TransactionTimestamp = DateTime.Now
            };

            await _rateRepository.AddTransactionRep(model);

            return model;
        }
    }
}
