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

        public UserController(IUserService userService)
        {
            _userService = userService;
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
            UserModel model =  await _userService.SearchByUserIdService(id);
            return model is not null ? Ok(model) : NotFound("ID cannot be null or empty");
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult<UserModel>> UpdateUser(string clientId, string firstName, string lastName)
        {
            UserModel updateModel = _userService.ConstructUserModelService(firstName, lastName);
            updateModel.ClientId = clientId;
            await _userService.UpdateUserService(clientId, updateModel);
            return updateModel is not null ? Ok(updateModel) : NotFound("ID cannot be null or empty");
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
            return id is not null ? Ok("User deleted") : NotFound("ID cannot be null or empty");
        }

        

    }
}
