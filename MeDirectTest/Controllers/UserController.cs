using MeDirectTest.Data.Repository;
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
        private readonly IUserRepository _userRepository;

        public UserController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult> AddUser([FromBody] UserModel userModel)
        {
            //_userService.ConstructUserModelService(model);
            if (string.IsNullOrEmpty(userModel.ClientId))
            {
                return BadRequest("The ID field cannot be empty");
            }
            
            await _userService.AddUserService(userModel);

            return Ok(userModel);
        }


        [HttpGet]
        [Route("SearchByUserId")]
        public async Task<ActionResult<UserModel>> SearchByUserId([FromQuery] string id)
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
