using MeDirectTest.Models;
using MeDirectTest.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace MeDirectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult> AddUser(string firstName, string lastName)
        {
            UserModel userModel = _userService.ConstructUserModelService(firstName, lastName);
            await _userService.AddUserService(userModel);

            _logger.Log(LogLevel.Information, $"Endpoint {nameof(AddUser)} finished");
            return Ok(userModel);
        }

        [HttpGet]
        [Route("SearchAllUsers")]
        public async Task<ActionResult<IEnumerable<UserModel>>> SearchAllUsers()
        {
            _logger.Log(LogLevel.Information, $"{nameof(SearchAllUsers)} method accessed");
            IEnumerable<UserModel> userModel = await _userService.SearchAllUsersService();

            _logger.Log(LogLevel.Information, $"Endpoint {nameof(SearchAllUsers)} finished");
            return Ok(userModel);
        }

        [HttpGet]
        [Route("SearchByUserId")]
        public async Task<ActionResult> SearchByUserId(string id)
        {
            _logger.Log(LogLevel.Information, $"{nameof(SearchByUserId)} method accessed");
            UserModel model = await _userService.SearchByUserIdService(id);
            
            _logger.Log(LogLevel.Information, $"Endpoint {nameof(SearchByUserId)} finished");
            return model is not null ? Ok(model) : NotFound("ID cannot be null or empty");
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult<UserModel>> UpdateUser(string clientId, string firstName, string lastName)
        {
            _logger.Log(LogLevel.Information, $"{nameof(UpdateUser)} method accessed");
            UserModel updateModel = _userService.ConstructUserModelService(firstName, lastName);
            updateModel.ClientId = clientId;

            await _userService.UpdateUserService(clientId, updateModel);

            _logger.Log(LogLevel.Information, $"Endpoint {nameof(UpdateUser)} finished");
            return updateModel is not null ? Ok(updateModel) : NotFound("ID cannot be null or empty");
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult<UserModel>> DeleteUser([FromQuery] string id)
        {
            _logger.Log(LogLevel.Information, $"{nameof(DeleteUser)} method accessed");
            if (String.IsNullOrEmpty(id))
            {
                _logger.Log(LogLevel.Error, $"ID field is required");
                return BadRequest("ID cannot be null or empty");
            }
            await _userService.DeleteUserService(id);

            _logger.Log(LogLevel.Information, $"Endpoint {nameof(DeleteUser)} finished");
            return id is not null ? Ok("User deleted") : NotFound("ID cannot be null or empty");
        }
    }
}
