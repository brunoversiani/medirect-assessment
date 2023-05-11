using MeDirectTest.Models;
using MeDirectTest.Service.Rates;
using MeDirectTest.Service.User;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> GetCurrencyConversion(string clientId, RateRequestModel rateRequestModel)
        {
            var validateId = await _userService.SearchByUserIdService(clientId);
            if (string.IsNullOrEmpty(validateId.ClientId))
            {
                return BadRequest("Invalid ID");
            }
            var response = await _rateService.IntegrationService(clientId, rateRequestModel);        

            return Ok(response);
        }
    }
}
