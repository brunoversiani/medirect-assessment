using MeDirectTest.Models;

namespace MeDirectTest.Data.Repository.Rates
{
    public interface IRateRepository
    {
        Task<TransactionModel> AddTransactionRep(TransactionModel model);
        Task<TransactionModel> SearchByTransactionIdRep(string transactionId);
        Task<IEnumerable<TransactionModel>> SearchTransactionsPerUserRep(string clientId);
        Task<IEnumerable<TransactionModel>> LastTenTransactionPerUser(IEnumerable<TransactionModel> listModel);
    }
}
