using MeDirectTest.Models;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<IEnumerable<TransactionModel>> SearchTransactionsPerUserRep(string clientId)
        {
            IEnumerable<TransactionModel> listModel = new List<TransactionModel>();
            listModel = _dataContext.TransactionContext.Where(x => x.TrClientId == clientId)
                                                       .OrderBy(x => x.TransactionTimestamp);

            if (listModel.IsNullOrEmpty())
            {
                throw new Exception("No transactions were found with the ID provided");
            }

            return listModel;
        }

        public async Task<IEnumerable<TransactionModel>> LastTenTransactionPerUser(IEnumerable<TransactionModel> listModel)
        {

            listModel.Take(10);

            if (listModel.IsNullOrEmpty())
            {
                throw new Exception("No transactions were found with the ID provided");
            }

            return listModel;
        }
    }
}
