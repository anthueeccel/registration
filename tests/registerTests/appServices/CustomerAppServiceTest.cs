using AutoMapper;
using FluentAssertions;
using NSubstitute;
using register.app.AutoMapper;
using register.app.Interfaces;
using register.app.Services;
using register.domain.Commands;
using register.domain.Entities;
using register.domain.Enum;
using register.domain.Interfaces;
using register.domain.Messaging;
using registerTests.builders;
using System.Threading.Tasks;
using Xunit;

namespace registerTests.appServices
{
    public class CustomerAppServiceTest
    {
        private ICustomerAppService _appService;
        private IMapper _mapper;
        private ICustomerRepository _repository;
        private IMediatorHandler _mediatorHandler;

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
                                                _mediatorHandler = Substitute.For<IMediatorHandler>());

        }

        [Fact(DisplayName = "Should Add Customer")]
        [Trait("AppService", "Customer")]
        public void should_add_Costumer()
        {            
            var model = new CustomerBuilder().BuildCustomer();

            var command = new AddCustomerCommand(model.FirstName,
                                                 model.LastName,
                                                 model.BirthDate,
                                                 model.Gender);
            
            _appService.AddAsync(model);

            var result = _mediatorHandler.SendCommand(command).IsCompletedSuccessfully;
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Should fail Add Customer (no FisrtName)")]
        [Trait("AppService", "Customer")]
        public async Task should_fail_add_Customer_validation_no_FirstName()
        {
            var model = new CustomerBuilder().BuildCustomer();

            model.FirstName = "";

            var command = new AddCustomerCommand(model.FirstName,
                                                 model.LastName,
                                                 model.BirthDate,
                                                 model.Gender);

            await _appService.AddAsync(model);
            
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn => dn.Value == "Sorry, first name can't be empty."));                
        }

        [Fact(DisplayName = "Should fail Add Customer (no BirthDate)")]
        [Trait("AppService", "Customer")]
        public async Task should_fail_add_Customer_validation_no_birthdate()
        {
            var model = new CustomerBuilder().BuildCustomer();

            model.BirthDate = default;

            var command = new AddCustomerCommand(model.FirstName,
                                                 model.LastName,
                                                 model.BirthDate,
                                                 model.Gender);

            await _appService.AddAsync(model);

            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn => dn.Value == "Sorry, the birth date can't be empty."));
        }

        [Fact(DisplayName = "Should fail Add Customer (no BirthDate)")]
        [Trait("AppService", "Customer")]
        public async Task should_fail_add_Customer_validation_invalid_gender()
        {
            var model = new CustomerBuilder().BuildCustomer();

            model.Gender = (GenderType)6;

            var command = new AddCustomerCommand(model.FirstName,
                                                 model.LastName,
                                                 model.BirthDate,
                                                 model.Gender);

            await _appService.AddAsync(model);

            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn => dn.Value == "Sorry, the Gender informed is invalid."));
        }
    }
}
