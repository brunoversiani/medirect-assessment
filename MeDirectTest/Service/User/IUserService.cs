using MeDirectTest.Models;

namespace MeDirectTest.Service.User
{
    public interface IUserService
    {
        Task<UserModel> AddUserService(UserModel model);
        Task<UserModel> SearchByUserIdService(string id);
        Task<UserModel> ConstructUserModelService(UserModel model);
    }
}
