using MeDirectTest.Data.Repository;
using MeDirectTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeDirectTest.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> AddUserService(UserModel model)
        {
            if (string.IsNullOrEmpty(model.ClientId))
            {
                throw new Exception($"Method: {nameof(AddUserService)}. Client ID not valid or already exists");
                //log
            }

            await _userRepository.AddUserRep(model);
            return model;
        }

        public async Task<UserModel> SearchByUserIdService(string id)
        {
            UserModel model = await _userRepository.SearchByUserIdRep(id);
            return model;
        }

        public async Task<UserModel> ConstructUserModelService(UserModel model)
        {
            model = new UserModel()
            {
                ClientId = model.ClientId,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            return model;
        }
    }
}
