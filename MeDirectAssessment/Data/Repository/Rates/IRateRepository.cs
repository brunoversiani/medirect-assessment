using MeDirectAssessment.Models;

namespace MeDirectAssessment.Data.Repository.Rates
{
    public interface IRateRepository
    {
        Task<TransactionModel> AddTransactionRep(TransactionModel model);
        Task<TransactionModel> SearchByTransactionIdRep(string transactionId);
        Task<TransactionModel> LastTransactionPerUserRep(string clientId);
        Task<IEnumerable<TransactionModel>> CacheTransactionModel(IEnumerable<TransactionModel> listModel);
    }
}
