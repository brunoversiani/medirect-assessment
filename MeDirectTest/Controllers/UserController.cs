using MeDirectTest.Data.Repository.User;
using MeDirectTest.Models;
using MeDirectTest.Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MeDirectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult> AddUser(string firstName, string lastName)
        {
            UserModel userModel = _userService.ConstructUserModelService(firstName, lastName);
                        
            await _userService.AddUserService(userModel);

            return Ok(userModel);
        }

        [HttpGet]
        [Route("SearchAllUsers")]
        public async Task<ActionResult<IEnumerable<UserModel>>> SearchAllUsers()
        {
            IEnumerable<UserModel> userModel = await _userService.SearchAllUsersService();
            return Ok(userModel);
        }

        [HttpGet]
        [Route("SearchByUserId")]
        public async Task<ActionResult> SearchByUserId(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("ID cannot be null or empty");
            }
            UserModel model =  await _userService.SearchByUserIdService(id);
            return Ok(model);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult<UserModel>> UpdateUser(string clientId, string firstName, string lastName)
        {
            if (String.IsNullOrEmpty(clientId))
            {
                return BadRequest("ID cannot be null or empty");
            }
            UserModel updateModel = _userService.ConstructUserModelService(firstName, lastName);
            updateModel.ClientId = clientId;
            await _userService.UpdateUserService(clientId, updateModel);
            return Ok(updateModel);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult<UserModel>> DeleteUser([FromQuery] string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("ID cannot be null or empty");
            }
            await _userService.DeleteUserService(id);
            return Ok("User deleted");
        }



    }
}
