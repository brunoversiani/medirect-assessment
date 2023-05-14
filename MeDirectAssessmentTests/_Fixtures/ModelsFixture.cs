using MeDirectAssessment.Models;

namespace MeDirectAssessmentTests._Fixtures
{
    public class ModelsFixture
    {


        public UserModel UserModelIsValid()
        {
            UserModel model = new UserModel()
            {
                ClientId = "0001",
                FirstName = "John",
                LastName = "Doe"
            };

            return model;
        }

        public RateRequestModel RateRequestModelIsValid()
        {
            RateRequestModel model = new RateRequestModel()
            {
                ClientIdRequest = "0001",
                From = "EUR",
                To = "BRL",
                Amount = 100,
                Date = DateTime.Now,
            };
            return model;
        }

        public TransactionModel TransactionModelIsValid()
        {
            TransactionModel model = new TransactionModel()
            {
                TransactionId = "Tr0001",
                TrClientId = "0001",
                TrFirstName = "John",
                TrLastName = "Doe",
                TrFromCurrency = "EUR",
                TrFromAmount = 5000,
                TrToCurrency = "BRL",
                TrRate = 3,
                TrRateTimestamp = DateTime.Now,
                TransactionTimestamp = DateTime.Now,
            };
            return model;
        }
    }
}
