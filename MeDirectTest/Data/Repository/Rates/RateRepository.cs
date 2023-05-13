using MeDirectTest.Models;
using Microsoft.Extensions.Caching.Memory;

namespace MeDirectTest.Data.Repository.Rates
{
    public class RateRepository : IRateRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<RateRepository> _logger;

        public RateRepository(DataContext dataContext, IMemoryCache memoryCache, ILogger<RateRepository> logger)
        {
            _dataContext = dataContext;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        IEnumerable<TransactionModel> listCache = new List<TransactionModel>();

        public async Task<TransactionModel> AddTransactionRep(TransactionModel model)
        {
            _logger.Log(LogLevel.Information, $"{nameof(AddTransactionRep)} method accessed");
            if (string.IsNullOrEmpty(model.TransactionId))
            {
                _logger.Log(LogLevel.Error, "The ID is empty or null");
                throw new Exception("An ID is required to process the transaction");
            }

            await _dataContext.AddAsync(model);
            await _dataContext.SaveChangesAsync();
            _logger.Log(LogLevel.Debug, "Transaction added to the database");

            return model;
        }

        public async Task<TransactionModel> SearchByTransactionIdRep(string transactionId)
        {
            _logger.Log(LogLevel.Information, $"{nameof(SearchByTransactionIdRep)} method accessed");
            var filter = await _dataContext.TransactionContext.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
            if (string.IsNullOrEmpty(filter.ToString()))
            {
                _logger.Log(LogLevel.Debug, "No users were found. Please, provide an existing user ID");
                return null;
            }
            _logger.Log(LogLevel.Debug, "Transaction retrieved from database");
            return filter;
        }

        public async Task<TransactionModel> LastTransactionPerUserRep(string clientId)
        {
            _logger.Log(LogLevel.Information, $"{nameof(LastTransactionPerUserRep)} method accessed");
            IEnumerable<TransactionModel> listModel = new List<TransactionModel>();
            listModel = _dataContext.TransactionContext.Where(x => x.TrClientId == clientId)
                                          .OrderByDescending(x => x.TrRateTimestamp);

            //var cacheList = await CacheTransactionModel(listModel);

            if ((DateTime.UtcNow - listModel.FirstOrDefault().TransactionTimestamp).TotalMinutes >= 30)
            {
                _logger.Log(LogLevel.Error, $"Method: {nameof(LastTransactionPerUserRep)}. \n TransactionID: {listModel.FirstOrDefault().TransactionId}. \n The transaction is older than 30 minutes");
                throw new Exception("The transaction is older than 30 minutes. Please register a new rate");
            }

            _logger.Log(LogLevel.Debug, $"Last transaction made by the user {listModel.FirstOrDefault().TrClientId} in the last 30 minutes found");
            TransactionModel model = listModel.FirstOrDefault();
            return model;
        }
                
        public async Task<IEnumerable<TransactionModel>> CacheTransactionModel(IEnumerable<TransactionModel> listModel)
        {
            _logger.Log(LogLevel.Information, $"{nameof(CacheTransactionModel)} method accessed");
            var cacheKey = "listKey";

            listCache = listModel;
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(20),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(10)
            };

            _memoryCache.Set(cacheKey, listCache, cacheExpiryOptions);

            return listCache;
        }
        /*
         * 
        private async Task<Product[]> GetProductsAsync() {
        var cacheKey = "transactionKey";
        //checks if cache entries exists
        if (!_memoryCache.TryGetValue(cacheKey, out transactionModel)) {
            //calling the server

            HttpClient client = new HttpClient();
            var stream = client.GetStreamAsync("https://northwind.vercel.app/api/products");
            productList = await JsonSerializer.DeserializeAsync<Product[]>(await stream);

            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions {
                AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(20)
            };
            //setting cache entries
            _memoryCache.Set(cacheKey, productList, cacheExpiryOptions);

            Console.WriteLine("Data from API (cache miss)");
        } else {
            Console.WriteLine("Data from CACHE (cache hit)");
        }

        return productList!;
        }

         * 
         */
    }
}
