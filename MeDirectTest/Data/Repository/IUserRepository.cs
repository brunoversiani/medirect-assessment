using MeDirectTest.Models;

namespace MeDirectTest.Data.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> AddUserRep(UserModel model);
        Task<bool> DeleteUserRep(string id);
        Task<UserModel> SearchByUserIdRep(string id);
    }
}
