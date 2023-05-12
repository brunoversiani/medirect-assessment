using MeDirectTest.Models;
using MeDirectTest.Service.Rates;
using MeDirectTest.Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace MeDirectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRateService _rateService;

        public RatesController(IUserService userService, IRateService rateService)
        {
            _userService = userService;
            _rateService = rateService;
        }

        [HttpPost]
        [Route("CurrencyConversion")]
        public async Task<ActionResult> CurrencyConversion(RateRequestModel rateRequestModel)
        {
                        
            var validateId = await _userService.SearchByUserIdService(rateRequestModel.ClientIdRequest);
            if (string.IsNullOrEmpty(validateId.ClientId))
            {
                return BadRequest("Invalid ID");
            }
            var response = await _rateService.IntegrationService(rateRequestModel.ClientIdRequest, rateRequestModel);        

            return Ok(response);
        }

        [HttpPost]
        [Route("NewExchangeWithOldRate")]
        public async Task<ActionResult> NewExchangeWithOldRate(string userId, double newAmount)
        {
            UserModel validateId = await _userService.SearchByUserIdService(userId);
            if (string.IsNullOrEmpty(validateId.ClientId))
            {
                return BadRequest("User does not exist or the ID is invalid");
            }

            var transactionModel = await _rateService.LastRegisterService(userId);
            var updatedModel = await _rateService.NewExchangeWithOldRateService(transactionModel, newAmount);

            return Ok(updatedModel);
        }
    }
}
