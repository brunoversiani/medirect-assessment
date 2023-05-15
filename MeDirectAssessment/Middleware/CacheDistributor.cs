//using MeDirectTest.Data.Repository.User;
//using MeDirectTest.Models;
//using Microsoft.Extensions.Caching.Memory;

//namespace MeDirectTest.Middleware
//{
//    public class CacheDistributor
//    {
//        private readonly MemoryCache _memoryCache;

//        public CacheDistributor(MemoryCache memoryCache, UserRepository )
//        {
//            _memoryCache = memoryCache;
//        }

//        public async Task<UserModel> CacheUserModel(UserModel userModel)
//        {
//            var cacheKey = "listKey";
//            UserModel cacheUser = new UserModel();

//            cacheUser = userModel;

//            var cacheExpiryOptions = new MemoryCacheEntryOptions
//            {
//                AbsoluteExpiration = DateTime.Now.AddSeconds(20),
//                Priority = CacheItemPriority.High,
//                SlidingExpiration = TimeSpan.FromSeconds(10)
//            };

//            _memoryCache.Set(cacheKey, cacheUser, cacheExpiryOptions);

//            return cacheUser;
//        }
//    }
//}
