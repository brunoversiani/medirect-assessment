using MeDirectTest.Models;
using MeDirectTest.Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeDirectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult> AddUser(string firstName, string lastName)
        {
            _logger.Log(LogLevel.Information, $"{nameof(AddUser)} method accessed");
            try
            {
                UserModel userModel = _userService.ConstructUserModelService(firstName, lastName);
                await _userService.AddUserService(userModel);

                return Ok(userModel);
            }
            catch (Exception e)
            {
                
                throw new Exception("123123123123123");
            }
            //if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            //{
            //    throw
            //}
            
        }

        [HttpGet]
        [Route("SearchAllUsers")]
        public async Task<ActionResult<IEnumerable<UserModel>>> SearchAllUsers()
        {
            _logger.Log(LogLevel.Information, $"{nameof(SearchAllUsers)} method accessed");
            IEnumerable<UserModel> userModel = await _userService.SearchAllUsersService();
            return Ok(userModel);
        }

        [HttpGet]
        [Route("SearchByUserId")]
        public async Task<ActionResult> SearchByUserId(string id)
        {
            _logger.Log(LogLevel.Information, $"{nameof(SearchByUserId)} method accessed");
            try
            {
                UserModel model = await _userService.SearchByUserIdService(id);
                return Ok(model);
            }
            catch (Exception e)
            {
                //if (_logger.IsEnabled(LogLevel.Error))
                //{
                //    _logger.LogError("The user ID cannot be empty");
                //}
                throw new Exception(e.Message);
            }
            
            //return model is not null ? Ok(model) : NotFound("ID cannot be null or empty");
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult<UserModel>> UpdateUser(string clientId, string firstName, string lastName)
        {
            _logger.Log(LogLevel.Information, $"{nameof(UpdateUser)} method accessed");
            UserModel updateModel = _userService.ConstructUserModelService(firstName, lastName);
            updateModel.ClientId = clientId;
            await _userService.UpdateUserService(clientId, updateModel);
            return updateModel is not null ? Ok(updateModel) : NotFound("ID cannot be null or empty");
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult<UserModel>> DeleteUser([FromQuery] string id)
        {
            _logger.Log(LogLevel.Information, $"{nameof(DeleteUser)} method accessed");
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("ID cannot be null or empty");
            }
            await _userService.DeleteUserService(id);
            return id is not null ? Ok("User deleted") : NotFound("ID cannot be null or empty");
        }

        /*
         * 
        private async Task<Product[]> GetProductsAsync() {
        var cacheKey = "transactionKey";
        //checks if cache entries exists
        if (!_memoryCache.TryGetValue(cacheKey, out transactionModel)) {
            //calling the server

            HttpClient client = new HttpClient();
            var stream = client.GetStreamAsync("https://northwind.vercel.app/api/products");
            productList = await JsonSerializer.DeserializeAsync<Product[]>(await stream);

            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions {
                AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(20)
            };
            //setting cache entries
            _memoryCache.Set(cacheKey, productList, cacheExpiryOptions);

            Console.WriteLine("Data from API (cache miss)");
        } else {
            Console.WriteLine("Data from CACHE (cache hit)");
        }

        return productList!;
        }

         * 
         */

    }
}
