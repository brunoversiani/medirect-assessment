using AutoFixture;
using MeDirectAssessment.Controllers;
using MeDirectAssessment.Models;
using MeDirectAssessment.Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace MeDirectAssessmentTests.Tests.User
{
    public class UserControllerTest
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _userService;
        private readonly Mock<ILogger<UserController>> _logger;
        private readonly Fixture _fixture;

        public UserControllerTest()
        {
            _userService = new Mock<IUserService>();
            _logger = new Mock<ILogger<UserController>>();
            _fixture = new Fixture();
            _userController = new UserController(_userService.Object, _logger.Object);
        }

        [Fact]
        public async void AddUser_WhenFieldsAreVallid_ShouldReturn200()
        {
            _userService.Setup(x => x.AddUserService(It.IsAny<UserModel>())).ReturnsAsync(It.IsAny<UserModel>());
            var _controller = await _userController.AddUser("First Name", "Last Name");
                                    
            var obj = _controller as ObjectResult;
            
            //Assert
            Assert.Equal(200, obj.StatusCode);
            _userService.Verify(x => x.AddUserService(It.IsAny<UserModel>()), Times.Once());
        }

        [Fact]
        public async void AddUser_WhenFieldsEmpty_ShouldReturn400()
        {
            var result = await _userController.AddUser("", "123");
            var obj = result as ObjectResult;
            
            //Assert
            Assert.Equal(400, obj.StatusCode);
        }


        [Fact]
        public async void SearchById_WhenRequestsUserById_ShouldReturnOk()
        {
            _userService.Setup(x => x.SearchAllUsersService()).ReturnsAsync(It.IsAny<IEnumerable<UserModel>>());
            
            var result = await _userController.SearchAllUsers();    
            var obj = result as ObjectResult;

            //Assert
            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async void SearchAllUsers_WhenRequestsUserList_ShouldReturnOk()
        {
            var id = _fixture.Create<string>();
            _userService.Setup(x => x.SearchByUserIdService(id)).ReturnsAsync(It.IsAny<UserModel>());

            var result = await _userController.SearchAllUsers();
            var obj = result as ObjectResult;

            //Assert
            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async void UpdateUser_WhenUpdateUserIsValid_ShouldReturnOk()
        {
            var id = _fixture.Create<string>();
            var model = _fixture.Create<UserModel>();
            var updatedModel = new UserModel()
            {
                ClientId = id,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            
            _userService.Setup(x => x.ConstructUserModelService(model.FirstName, model.LastName)).Returns(updatedModel);
            _userService.Setup(x => x.UpdateUserService(id, model)).ReturnsAsync(updatedModel);

            var result = await _userController.UpdateUser(id, model.FirstName, model.LastName);
            var obj = result as ObjectResult;

            //Assert
            Assert.Equal(200, obj.StatusCode);
        }

        [Fact]
        public async void DeleteUser_WhenDeleteUserisTrue_ShouldReturnOk()
        {
            var id = _fixture.Create<string>();
            

            _userService.Setup(x => x.DeleteUserService(id)).ReturnsAsync(true);

            var result = await _userController.DeleteUser(id);
            var obj = result as ObjectResult;

            //Assert
            Assert.Equal(200, obj.StatusCode);
        }
    }
}
