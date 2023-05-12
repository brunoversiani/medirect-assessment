using MeDirectTest.Models;

namespace MeDirectTest.Data.Repository.Rates
{
    public class RateRepository : IRateRepository
    {
        private readonly DataContext _dataContext;

        public RateRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<TransactionModel> AddTransactionRep(TransactionModel model)
        {
            if (string.IsNullOrEmpty(model.TransactionId))
            {
                throw new Exception("An ID is required to process the transaction");
            }

            await _dataContext.AddAsync(model);
            await _dataContext.SaveChangesAsync();
            return model;
        }

        public async Task<TransactionModel> SearchByTransactionIdRep(string transactionId)
        {
            var filter = _dataContext.TransactionContext.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
            if (string.IsNullOrEmpty(filter.ToString()))
            {
                throw new Exception("No transactions were found with the ID provided");
            }

            return await filter;
        }

        public async Task<TransactionModel> LastTransactionPerUserRep(string clientId)
        {
            IEnumerable<TransactionModel> listModel = new List<TransactionModel>();
            listModel = _dataContext.TransactionContext.Where(x => x.TrClientId == clientId)
                                          .OrderByDescending(x => x.TrRateTimestamp);

            if ((DateTime.UtcNow - listModel.FirstOrDefault().TransactionTimestamp).TotalMinutes >= 30)
            {
                //log
                throw new Exception("The rate is older than 30 minutes. Please register a new rate");
            }

            //log This exchange rate is XX minutes old
            TransactionModel model = listModel.FirstOrDefault();
            return model;
        }        
    }
}
