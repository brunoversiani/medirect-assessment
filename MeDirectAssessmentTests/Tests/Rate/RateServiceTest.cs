using AutoFixture;
using MeDirectAssessment.Data.Repository.Rates;
using MeDirectAssessment.Models;
using MeDirectAssessment.Service.Rates;
using MeDirectAssessment.Service.User;
using MeDirectAssessmentTests._Fixtures;
using Microsoft.Extensions.Logging;

namespace MeDirectAssessmentTests.Tests.Rate
{
    public class RateServiceTest
    {
        private readonly RateService _rateService;
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IRateRepository> _rateRepository;
        private readonly Mock<ILogger<RateService>> _logger;
        private readonly Fixture _fixture;
        private readonly ModelsFixture _modelsFixture;

        public RateServiceTest()
        {
            _userService = new Mock<IUserService>();
            _rateRepository = new Mock<IRateRepository>();
            _logger = new Mock<ILogger<RateService>>();
            _fixture = new Fixture();
            _modelsFixture = new ModelsFixture();
            _rateService = new RateService(_userService.Object, _rateRepository.Object, _logger.Object);
        }

        [Fact]
        public async void IntegrationService_WhenRequestIsValid_ShouldReturnRestResponse()
        {
            var userModel = _modelsFixture.UserModelIsValid();
            var validRequestModel = _modelsFixture.RateRequestModelIsValid();
            var requestModel = _fixture.Create<RateRequestModel>();


            _userService.Setup(x => x.SearchByUserIdService(validRequestModel.ClientIdRequest)).ReturnsAsync(userModel);
            
            var result = await _rateService.IntegrationService(validRequestModel.ClientIdRequest, validRequestModel);

            Assert.NotNull(result);
        }

        [Fact]
        public async void LastRegisterService_WhenIdIsValid_ShouldReturnModel()
        {
            var id = _fixture.Create<string>();
            var transactionModel = _modelsFixture.TransactionModelIsValid();

            _rateRepository.Setup(x => x.LastTransactionPerUserRep(id)).ReturnsAsync(transactionModel);

            var result = await _rateService.LastRegisterService(id);

            Assert.NotNull(result);
            _rateRepository.Verify(x => x.LastTransactionPerUserRep(It.IsAny<string>()), Times.Once);
        }   
        
    }
}
