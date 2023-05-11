using MeDirectTest.Models;

namespace MeDirectTest.Data.Repository.User
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

            await _dataContext.AddAsync(model);
            await _dataContext.SaveChangesAsync();
            return model;
        }

        public async Task<List<UserModel>> SearchAllUsersRep()
        {
            return await _dataContext.UserContext.ToListAsync();
        }

        public async Task<UserModel> SearchByUserIdRep(string id)
        {
            UserModel model = await _dataContext.UserContext.FirstOrDefaultAsync(x => x.ClientId == id);
            if (model == null)
            {
                return null;
            }
            return model;
        }

        public async Task<UserModel> UpdateUserRep(UserModel referenceModel)
        {
            _dataContext.UserContext.Update(referenceModel);
            _dataContext.SaveChanges();
            return referenceModel;
        }

        public async Task<bool> DeleteUserRep(UserModel userModel)
        {            
            _dataContext.UserContext.Remove(userModel);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
