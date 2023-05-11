using MeDirectTest.Models;

namespace MeDirectTest.Service.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> SearchAllUsersService();
        Task<UserModel> AddUserService(UserModel model);
        Task<UserModel> SearchByUserIdService(string id);
        Task<UserModel> UpdateUserService(string clientId, UserModel userModel);
        Task<bool> DeleteUserService(string clientId);
        UserModel ConstructUserModelService(string firstName, string lastName);
    }
}
