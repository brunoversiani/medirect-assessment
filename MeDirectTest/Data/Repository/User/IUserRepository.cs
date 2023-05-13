using MeDirectTest.Models;

namespace MeDirectTest.Data.Repository.User
{
    public interface IUserRepository
    {
        Task<UserModel> AddUserRep(UserModel model);
        Task<IEnumerable<UserModel>> SearchAllUsersRep();
        Task<UserModel> SearchByUserIdRep(string id);
        Task<UserModel> UpdateUserRep(UserModel userModel);
        Task<bool> DeleteUserRep(UserModel userModel);


    }
}
