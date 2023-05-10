using MeDirectTest.Models;

namespace MeDirectTest.Repository
{
    public interface IRateRepository
    {
        Task<TransactionModel> AddTransactionRep(TransactionModel model);
        Task<TransactionModel> SearchByTransactionIdRep(string transactionId);
        Task<IEnumerable<TransactionModel>> SearchTransactionsPerUserRep(string clientId);
        Task<IEnumerable<TransactionModel>> LastTenTransactionPerUser(IEnumerable<TransactionModel> listModel);
    }
}
