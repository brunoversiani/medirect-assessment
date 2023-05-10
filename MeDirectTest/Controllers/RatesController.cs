using MeDirectTest.Models;
using MeDirectTest.Service.Rates;
using MeDirectTest.Service.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace MeDirectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRateService _rateService;
        private readonly DataContext _dataContext;

        public RatesController(IUserService userService, IRateService rateService, DataContext dataContext)
        {
            _userService = userService;
            _rateService = rateService;
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("CurrencyConversion")]
        public async Task<ActionResult> GetCurrencyConversion(string clientId, string from, string to, string amount, string? date)
        {
            var validateId = await _userService.SearchByUserIdService(clientId);
            if (string.IsNullOrEmpty(validateId.ClientId))
            {
                return BadRequest("Invalid ID");
            }

            var client = new RestClient($"https://api.apilayer.com/exchangerates_data/convert?to={to}&from={from}&amount={amount}");

            var request = new RestRequest("", Method.Get);
            request.AddHeader("apikey", "6JTABFgsuQ1LK0DTmRyMg6xYd1SYmM1s");

            RestResponse response = client.Execute(request);
            RateModel rateModel = JsonConvert.DeserializeObject<RateModel>(response.Content);
            Console.WriteLine(response.Content);

            await _rateService.ConstructTransactionModel(clientId, rateModel);

            return Ok(response.Content);
        }
    }
}
