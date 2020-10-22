using AutoMapper;
using NSubstitute;
using register.app.AutoMapper;
using register.app.Interfaces;
using register.app.Services;
using register.domain.Interfaces;
using Xunit;

namespace registerTests.appServices
{
    public class CustomerAppServiceTest
    {
        private ICustomerAppService _appService;
        private IMapper _mapper;
        private ICustomerRepository _repository;
        private IMediatorHandler _mediator;

        public CustomerAppServiceTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToViewModelAutoMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _appService = new CustomerAppService(_repository = Substitute.For<ICustomerRepository>(),
                                                _mapper,
                                                _mediator = Substitute.For<IMediatorHandler>());

        }

        [Fact(DisplayName = "Should Add Customer")]
        [Trait("AppService", "Customer")]
        public void should_add_Costumer()
        {
        }
    }
}
