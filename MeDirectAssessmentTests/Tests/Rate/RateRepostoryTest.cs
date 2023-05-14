using AutoFixture;
using MeDirectAssessment.Data;
using MeDirectAssessment.Data.Repository.Rates;
using MeDirectAssessmentTests._Fixtures;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MeDirectAssessmentTests.Tests.Rate
{
    public class RateRepostoryTest
    {
        private RateRepository _rateRepository;
        private Mock<DataContext> _context;
        private Mock<IMemoryCache> _memoryCache;
        private Mock<ILogger<RateRepository>> _logger;
        private Fixture _fixture;
        private ModelsFixture _modelsFixture;

        public RateRepostoryTest()
        {
            _context = new Mock<DataContext>();
            _memoryCache = new Mock<IMemoryCache>();
            _logger = new Mock<ILogger<RateRepository>>();
            _fixture = new Fixture();
            _modelsFixture = new ModelsFixture();
            _rateRepository = new RateRepository(_context.Object, _memoryCache.Object, _logger.Object);
        }

        //TODO


    }
}
