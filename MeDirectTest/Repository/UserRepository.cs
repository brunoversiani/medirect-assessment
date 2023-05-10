using MeDirectTest.Models;

namespace MeDirectTest.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<UserModel> AddUserRep(UserModel model)
        {
            if (string.IsNullOrEmpty(model.ClientId))
            {
                model.ClientId = Guid.NewGuid().ToString();
            }

            await _dataContext.AddAsync(model);
            await _dataContext.SaveChangesAsync();
            return model;
        }

        public async Task<UserModel> SearchByUserIdRep(string id)
        {
            var filter = _dataContext.UserContext.FirstOrDefaultAsync(x => x.ClientId == id);
            if (String.IsNullOrEmpty(filter.ToString()))
            {
                throw new Exception("No clients were found with the ID provided");
            }
            else
            {
                return await filter;
            }
        }

        public async Task<bool> DeleteUserRep(string id)
        {
            UserModel userById = await SearchByUserIdRep(id);
            if (userById == null)
            {
                throw new Exception("User not found");
                //log
            }

            _dataContext.UserContext.Remove(userById);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
