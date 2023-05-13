using MeDirectTest.Models;

namespace MeDirectTest.Data.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(DataContext dataContext, ILogger<UserRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        
        public async Task<UserModel> AddUserRep(UserModel model)
        {
            //await _cacheDistributor.CacheUserModel(model);

            await _dataContext.AddAsync(model);
            await _dataContext.SaveChangesAsync();

            _logger.Log(LogLevel.Debug, $"ClientID {model.ClientId}: Client was successfully added to the database");
            return model;
        }

        public async Task<List<UserModel>> SearchAllUsersRep()
        {
            _logger.Log(LogLevel.Debug, "Users were listed successfully");
            return await _dataContext.UserContext.ToListAsync();
        }

        public async Task<UserModel> SearchByUserIdRep(string id)
        {

            UserModel model = await _dataContext.UserContext.FirstOrDefaultAsync(x => x.ClientId == id);
            if (model == null)
            {
                _logger.Log(LogLevel.Warning, "No users were found with the ID provided");
                return null;
            }
            _logger.Log(LogLevel.Debug, "User found successfully");
            return model;
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

        
    }
}
