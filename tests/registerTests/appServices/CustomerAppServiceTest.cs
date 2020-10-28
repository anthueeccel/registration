using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using register.app.AutoMapper;
using register.app.Interfaces;
using register.app.Services;
using register.domain.Commands;
using register.domain.Entities;
using register.domain.Enum;
using register.domain.Interfaces;
using register.domain.Messaging;
using registerTests.builders;
using System;
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
        public async Task should_add_Costumer()
        {            
            var model = new CustomerBuilder().BuildCustomer();

            var command = new AddCustomerCommand(model.FirstName,
                                                 model.LastName,
                                                 model.BirthDate,
                                                 model.Gender);
            
            await _appService.AddAsync(model);

            await _mediatorHandler
                .Received(1)
                .SendCommand(Arg.Is<AddCustomerCommand>(d => d.FirstName == model.FirstName
                                                          && d.LastName == model.LastName
                                                          && d.BirthDate == model.BirthDate
                                                          && d.Gender == model.Gender));

        }

        [Fact(DisplayName = "Should fail Add Customer (no FisrtName)")]
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
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn => 
                    dn.Value == "Sorry, first name can't be empty."));                
        }

        [Fact(DisplayName = "Should fail Add Customer (no BirthDate)")]
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
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn => 
                    dn.Value == "Sorry, the birth date can't be empty."));
        }

        [Fact(DisplayName = "Should fail Add Customer (invalid Gender)")]
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
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn => 
                    dn.Value == "Sorry, the Gender informed is invalid."));
        }

        [Fact(DisplayName = "Should Update Customer")]
        public async Task should_update_Customer()
        {
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.BirthDate,
                                                    model.Gender);

            var dbEntity = _mapper.Map<Customer>(model);
            _repository.GetById(model.Id).Returns(dbEntity);

            await _appService.UpdateAsync(model);

            await _mediatorHandler
                .Received(1)
                .SendCommand(Arg.Is<UpdateCustomerCommand>(d => d.Id == model.Id
                                                             && d.FirstName == model.FirstName
                                                             && d.LastName == model.LastName
                                                             && d.BirthDate == model.BirthDate
                                                             && d.Gender == model.Gender));            
        }

        [Fact(DisplayName = "Should fail Update Customer (no Id)")]
        public async Task should_fail_update_Customer_no_id()
        {
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = default;

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.BirthDate,
                                                    model.Gender);

            var dbEntity = _mapper.Map<Customer>(model);
            _repository.GetById(model.Id).Returns(dbEntity);

            await _appService.UpdateAsync(model);

            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Please, must inform the Id."));
        }

        [Fact(DisplayName = "Should fail Update Customer (no FirstName)")]
        public async Task should_fail_update_Customer_no_FirstName()
        {
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();
            model.FirstName = "";

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.BirthDate,
                                                    model.Gender);

            var dbEntity = _mapper.Map<Customer>(model);
            _repository.GetById(model.Id).Returns(dbEntity);

            await _appService.UpdateAsync(model);

            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn => 
                    dn.Value == "Sorry, first name can't be empty."));
        }

        [Fact(DisplayName = "Should fail Update Customer (no BirthDate)")]
        public async Task should_fail_update_Customer_no_BirthDate()
        {
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();
            model.BirthDate = default;

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.BirthDate,
                                                    model.Gender);

            var dbEntity = _mapper.Map<Customer>(model);
            _repository.GetById(model.Id).Returns(dbEntity);

            await _appService.UpdateAsync(model);

            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Sorry, the birth date can't be empty."));
        }

        [Fact(DisplayName = "Should fail Update Customer (invalid Gender)")]
        public async Task should_fail_update_Customer_invalid_Gender()
        {
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();
            model.Gender = (GenderType)99;

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.BirthDate,
                                                    model.Gender);

            var dbEntity = _mapper.Map<Customer>(model);
            _repository.GetById(model.Id).Returns(dbEntity);

            await _appService.UpdateAsync(model);

            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Sorry, the Gender informed is invalid."));
        }

        [Fact(DisplayName = "Should fail Update Customer (register not founf)")]
        public async Task should_fail_update_Customer_not_found()
        {
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.BirthDate,
                                                    model.Gender);

            _repository.GetById(model.Id).ReturnsNull();

            await _appService.UpdateAsync(model);

            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Customer not found."));
        }
    }
}
