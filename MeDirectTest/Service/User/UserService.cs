using MeDirectTest.Data.Repository.User;
using MeDirectTest.Models;

namespace MeDirectTest.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserModel>> SearchAllUsersService()
        {
            return await _userRepository.SearchAllUsersRep();
        }

        public async Task<UserModel> AddUserService(UserModel model)
        {
            UserModel filter = await SearchByUserIdService(model.ClientId);
            if (filter !=null )
            {
                //log
                throw new Exception($"Method: {nameof(AddUserService)}. Client ID already exists");
            }

            await _userRepository.AddUserRep(model);
            return model;
        }

        public async Task<UserModel> SearchByUserIdService(string id)
        {
            UserModel model = await _userRepository.SearchByUserIdRep(id);
            return model;
        }

        public async Task<UserModel> UpdateUserService(string clientId, UserModel userModel)
        {
            UserModel referenceModel = await SearchByUserIdService(clientId);
            if (referenceModel == null)
            {
                //log
                throw new Exception("User not found");
            }

            referenceModel.FirstName = userModel.FirstName;
            referenceModel.LastName = userModel.LastName;

            await _userRepository.UpdateUserRep( referenceModel);
            return userModel;
        }

        public async Task<bool> DeleteUserService(string clientId)
        {
            UserModel userModel = await SearchByUserIdService(clientId);
            if (userModel == null)
            {
                //log
                throw new Exception("User not found");
            }

            await _userRepository.DeleteUserRep(userModel);
            return true;
        }

        public UserModel ConstructUserModelService(string firstName, string lastName)
        {
            UserModel userModel = new UserModel()
            {
                ClientId = Guid.NewGuid().ToString(),
                FirstName = firstName,
                LastName = lastName
            };
            return userModel;
        }
    }
}
