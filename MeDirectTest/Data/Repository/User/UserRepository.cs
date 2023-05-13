﻿using MeDirectTest.Models;
using Microsoft.Extensions.Caching.Memory;

namespace MeDirectTest.Data.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<UserRepository> _logger;
        private readonly IMemoryCache _memoryCache;

        public UserRepository(DataContext dataContext, ILogger<UserRepository> logger, IMemoryCache memoryCache)
        {
            _dataContext = dataContext;
            _logger = logger;
            _memoryCache = memoryCache;
        }
        
        public async Task<UserModel> AddUserRep(UserModel model)
        {
            //await _cacheDistributor.CacheUserModel(model);
            _memoryCache.Remove("allUsersKey");
            await _dataContext.AddAsync(model);
            await _dataContext.SaveChangesAsync();
            AllUsersCache();

            _logger.Log(LogLevel.Debug, $"ClientID {model.ClientId}: Client was successfully added to the database");
            return model;
        }

        public async Task<IEnumerable<UserModel>> SearchAllUsersRep()
        {
            var listAllUsers = await AllUsersCache();
            _logger.Log(LogLevel.Debug, "Users were listed successfully");
            return listAllUsers;
        }

        public async Task<UserModel> SearchByUserIdRep(string id)
        {
            UserModel cacheUserModel = new UserModel();

            if (_memoryCache.TryGetValue(keyAllUsers, out IEnumerable<UserModel> allUsersModel))
            {
                cacheUserModel = allUsersModel.FirstOrDefault(x => x.ClientId == id);

                if (cacheUserModel == null)
                {
                    _logger.Log(LogLevel.Warning, "No users were found with the ID provided");
                    return null;
                }
                else
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(120))
                    .SetPriority(CacheItemPriority.Normal);
                    _memoryCache.Set(keyUserId, cacheUserModel, cacheEntryOptions);

                    _logger.Log(LogLevel.Information, "Users found from AllUsers cache");
                    return cacheUserModel;
                }

            }
            else
            {
                UserModel model = await _dataContext.UserContext.FirstOrDefaultAsync(x => x.ClientId == id);
                if (model == null)
                {
                    _logger.Log(LogLevel.Warning, "No users were found with the ID provided");
                    return null;
                }
                return model;
            }




            //UserModel cacheUserById = await UserByIdCache(id);

            //if (cacheUserById == null)
            //{
            //    UserModel model = await _dataContext.UserContext.FirstOrDefaultAsync(x => x.ClientId == id);
            //    if (model == null)
            //    {
            //        _logger.Log(LogLevel.Warning, "No users were found with the ID provided");
            //        return null;
            //    }
            //    return model;
            //}
            //else
            //{
            //    return cacheUserById;
            //}


            //_logger.Log(LogLevel.Debug, "User found successfully");
            //return model;
        }

        public async Task<UserModel> UpdateUserRep(UserModel referenceModel)
        {
            _dataContext.UserContext.Update(referenceModel);
            _dataContext.SaveChanges();

            _logger.Log(LogLevel.Debug, "User updated successfully");
            return referenceModel;
        }

        public async Task<bool> DeleteUserRep(UserModel userModel)
        {            
            _dataContext.UserContext.Remove(userModel);
            await _dataContext.SaveChangesAsync();

            _logger.Log(LogLevel.Debug, "User deleted successfully");
            return true;
        }

        private string keyAllUsers = "keyAllUsers";
        private string keyUserId = "keyUserId";

        private async Task<IEnumerable<UserModel>> AllUsersCache()
        {
            
            if (_memoryCache.TryGetValue(keyAllUsers, out IEnumerable<UserModel> allUsersModel))
            {
                _logger.Log(LogLevel.Information, "Users found in cache");
                return allUsersModel;
            }
            else
            {
                allUsersModel = await _dataContext.UserContext.ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(120))
                    .SetPriority(CacheItemPriority.Normal);
                _memoryCache.Set(keyAllUsers, allUsersModel, cacheEntryOptions);

                return allUsersModel;
            }
        }

        private async Task<UserModel> UserByIdCache(string id)
        {
            UserModel cacheUserModel = new UserModel();
            
            if (_memoryCache.TryGetValue(keyAllUsers, out IEnumerable<UserModel> allUsersModel))
            {
                cacheUserModel = allUsersModel.FirstOrDefault(x => x.ClientId == id);

                if(cacheUserModel == null)
                {
                    _logger.Log(LogLevel.Warning, "No users were found with the ID provided");
                    return null;
                }
                else
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(120))
                    .SetPriority(CacheItemPriority.Normal);
                    _memoryCache.Set(keyUserId, cacheUserModel, cacheEntryOptions);

                    _logger.Log(LogLevel.Information, "Users found from AllUsers cache");
                    return cacheUserModel;
                }
                
            }
            else
            {
                UserModel model = await _dataContext.UserContext.FirstOrDefaultAsync(x => x.ClientId == id);
                if (model == null)
                {
                    _logger.Log(LogLevel.Warning, "No users were found with the ID provided");
                    return null;
                }
                return model;
            }

        }


    }
}
