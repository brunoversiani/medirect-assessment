using AutoFixture;
using MeDirectAssessment.Controllers;
using MeDirectAssessment.Models;
using MeDirectAssessment.Service.Rates;
using MeDirectAssessment.Service.User;
using MeDirectAssessmentTests._Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MeDirectAssessmentTests.Tests.Rate
{
    public class RateControllerTests
    {
        private readonly RatesController _ratesController;
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IRateService> _rateService;
        private readonly Mock<ILogger<RatesController>> _logger;
        private readonly ModelsFixture _modelsFixture;
        private readonly Fixture _fixture;

        public RateControllerTests()
        {
            _userService = new Mock<IUserService>();
            _rateService = new Mock<IRateService>();
            _logger = new Mock<ILogger<RatesController>>();
            _fixture = new Fixture();
            _modelsFixture = new ModelsFixture();
            _ratesController = new RatesController(_userService.Object, _rateService.Object, _logger.Object);
        }

        [Fact]
        public async void CurrencyConversion_WhenRequestIsValid_ShouldReturnOk()
        {
            var FixtureRequestModel = _fixture.Create<RateRequestModel>();
            var FixtureUserModel = _fixture.Create<UserModel>();
            _userService.Setup(x => x.SearchByUserIdService(FixtureRequestModel.ClientIdRequest)).ReturnsAsync(FixtureUserModel);
            _rateService.Setup(x => x.IntegrationService(It.IsAny<string>(), FixtureRequestModel)).ReturnsAsync(It.IsAny<RestSharp.RestResponse>());        

            var result = await _ratesController.CurrencyConversion(FixtureRequestModel);
            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
            _rateService.Verify(x => x.IntegrationService(It.IsAny<string>(), It.IsAny<RateRequestModel>()), Times.Once());
        }

        [Fact]
        public async void NewExchangeWithOldRate_WhenRequestIsValid_ShouldReturnOk()
        {
            var FixtureTransactionModel = _modelsFixture.TransactionModelIsValid();
            var FixtureUserModel = _modelsFixture.UserModelIsValid();

            string id = FixtureTransactionModel.TrClientId;
            double amount = _fixture.Create<double>();

            _userService.Setup(x => x.SearchByUserIdService(id)).ReturnsAsync(FixtureUserModel);
            _rateService.Setup(x => x.LastRegisterService(id)).ReturnsAsync(FixtureTransactionModel);
            _rateService.Setup(x => x.NewExchangeWithOldRateService(FixtureTransactionModel, amount)).ReturnsAsync(It.IsAny<TransactionModel>());

            var result = await _ratesController.NewExchangeWithOldRate(id, amount);
            var obj = result as ObjectResult;

            Assert.Equal(200, obj.StatusCode);
            _rateService.Verify(x => x.LastRegisterService(id), Times.Once());
            _rateService.Verify(x => x.NewExchangeWithOldRateService(FixtureTransactionModel, amount), Times.Once());
        }
    }
}
