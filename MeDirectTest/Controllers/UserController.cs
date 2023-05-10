using MeDirectTest.Models;
using MeDirectTest.Repository;
using MeDirectTest.Service.User;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [Route("AddUser")]
        public async Task<ActionResult> AddUser([FromQuery] string id, string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("The ID field cannot be empty");
            }
            var userModel = await _userService.ConstructUserModelService(id, firstName, lastName);
            await _userService.AddUserService(userModel);

            return Ok(userModel);
        }


        [HttpGet]
        [Route("SerchByUserId")]
        public async Task<ActionResult<UserModel>> SerchByUserId([FromQuery] string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("ID cannot be null or empty");
            }
            UserModel model = await _userService.SearchByUserIdService(id);
            return Ok(model);
        }

        

    }
}
