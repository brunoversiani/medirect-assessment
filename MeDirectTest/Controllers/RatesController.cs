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
        private readonly ILogger<RatesController> _logger;

        public RatesController(IUserService userService, IRateService rateService, ILogger<RatesController> logger)
        {
            _userService = userService;
            _rateService = rateService;
            _logger = logger;
        }

        [HttpPost]
        [Route("CurrencyConversion")]
        public async Task<ActionResult> CurrencyConversion(RateRequestModel rateRequestModel)
        {
            _logger.Log(LogLevel.Information, $"{nameof(CurrencyConversion)} method accessed");
            var validateId = await _userService.SearchByUserIdService(rateRequestModel.ClientIdRequest);
            if (string.IsNullOrEmpty(validateId.ClientId))
            {
                _logger.Log(LogLevel.Warning, $"{nameof(CurrencyConversion)} endpoint: ID empty or null");
                return BadRequest("Invalid ID");
            }
            var response = await _rateService.IntegrationService(rateRequestModel.ClientIdRequest, rateRequestModel);

            _logger.Log(LogLevel.Information, $"{nameof(CurrencyConversion)} endpoint concluded");
            return Ok(response);
        }

        [HttpPost]
        [Route("NewExchangeWithOldRate")]
        public async Task<ActionResult> NewExchangeWithOldRate(string userId, double newAmount)
        {
            _logger.Log(LogLevel.Information, $"{nameof(NewExchangeWithOldRate)} method accessed");
            UserModel validateId = await _userService.SearchByUserIdService(userId);
            if (string.IsNullOrEmpty(validateId.ClientId))
            {
                _logger.Log(LogLevel.Warning, $"{nameof(NewExchangeWithOldRate)} endpoint: ID empty or null");
                return BadRequest("User does not exist or the ID is invalid");
            }

            var transactionModel = await _rateService.LastRegisterService(userId);
            var updatedModel = await _rateService.NewExchangeWithOldRateService(transactionModel, newAmount);

            _logger.Log(LogLevel.Information, $"{nameof(NewExchangeWithOldRate)} endpoint concluded");
            return Ok(updatedModel);
        }
    }
}
