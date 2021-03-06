﻿using AutoMapper;
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
using System.Collections.Generic;
using System.Linq;
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
        public async Task Should_add_Costumer()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();

            var command = new AddCustomerCommand(model.FirstName,
                                                 model.LastName,
                                                 model.Birthdate,
                                                 model.Gender);
            //Act
            await _appService.AddAsync(model);

            //Assert
            await _mediatorHandler
                .Received(1)
                .SendCommand(Arg.Is<AddCustomerCommand>(d => d.FirstName == model.FirstName
                                                          && d.LastName == model.LastName
                                                          && d.BirthDate == model.Birthdate
                                                          && d.Gender == model.Gender));

        }

        [Fact(DisplayName = "Should fail Add Customer (no FisrtName)")]
        public async Task Should_fail_add_Customer_validation_no_FirstName()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();

            model.FirstName = "";

            var command = new AddCustomerCommand(model.FirstName,
                                                 model.LastName,
                                                 model.Birthdate,
                                                 model.Gender);
            //Act
            await _appService.AddAsync(model);

            //Assert
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Sorry, first name can't be empty."));
        }

        [Fact(DisplayName = "Should fail Add Customer (no BirthDate)")]
        public async Task Should_fail_add_Customer_validation_no_birthdate()
        {
            //Arange
            var model = new CustomerBuilder().BuildCustomer();

            model.Birthdate = default;

            var command = new AddCustomerCommand(model.FirstName,
                                                 model.LastName,
                                                 model.Birthdate,
                                                 model.Gender);

            //Act
            await _appService.AddAsync(model);


            //Assert
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Sorry, the birth date can't be empty."));
        }

        [Fact(DisplayName = "Should fail Add Customer (invalid Gender)")]
        public async Task Should_fail_add_Customer_validation_invalid_gender()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();

            model.Gender = (GenderType)6;

            var command = new AddCustomerCommand(model.FirstName,
                                                 model.LastName,
                                                 model.Birthdate,
                                                 model.Gender);

            //Act
            await _appService.AddAsync(model);

            //Assert
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Sorry, the Gender informed is invalid."));
        }

        [Fact(DisplayName = "Should fail Add Customer (duplicated)")]
        public async Task Should_fail_add_Customer_validation_duplicated_customer()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();

            var command = new AddCustomerCommand(model.FirstName,
                                                 model.LastName,
                                                 model.Birthdate,
                                                 model.Gender);
            var dbEntity = _mapper.Map<Customer>(model);

            _repository.Query().Returns(new List<Customer> { dbEntity }.AsQueryable());

            //Act
            await _appService.AddAsync(model);


            //Assert
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Customer already registered."));
        }

        [Fact(DisplayName = "Should Update Customer")]
        public async Task Should_update_Customer()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.Birthdate,
                                                    model.Gender);

            //Act
            var dbEntity = _mapper.Map<Customer>(model);
            _repository.GetById(model.Id).Returns(dbEntity);

            await _appService.UpdateAsync(model);

            //Assert
            await _mediatorHandler
                .Received(1)
                .SendCommand(Arg.Is<UpdateCustomerCommand>(d => d.Id == model.Id
                                                             && d.FirstName == model.FirstName
                                                             && d.LastName == model.LastName
                                                             && d.BirthDate == model.Birthdate
                                                             && d.Gender == model.Gender));
        }

        [Fact(DisplayName = "Should fail Update Customer (no Id)")]
        public async Task Should_fail_update_Customer_no_id()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = default;

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.Birthdate,
                                                    model.Gender);

            var dbEntity = _mapper.Map<Customer>(model);
            _repository.GetById(model.Id).Returns(dbEntity);

            //Act
            await _appService.UpdateAsync(model);


            //Assert
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Please, must inform the Id."));
        }

        [Fact(DisplayName = "Should fail Update Customer (no FirstName)")]
        public async Task Should_fail_update_Customer_no_FirstName()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();
            model.FirstName = "";

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.Birthdate,
                                                    model.Gender);

            var dbEntity = _mapper.Map<Customer>(model);
            _repository.GetById(model.Id).Returns(dbEntity);

            //Act
            await _appService.UpdateAsync(model);

            //Assert
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Sorry, first name can't be empty."));
        }

        [Fact(DisplayName = "Should fail Update Customer (no BirthDate)")]
        public async Task Should_fail_update_Customer_no_BirthDate()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();
            model.Birthdate = default;

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.Birthdate,
                                                    model.Gender);

            var dbEntity = _mapper.Map<Customer>(model);
            _repository.GetById(model.Id).Returns(dbEntity);

            //Act
            await _appService.UpdateAsync(model);

            //Assert
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Sorry, the birth date can't be empty."));
        }

        [Fact(DisplayName = "Should fail Update Customer (invalid Gender)")]
        public async Task Should_fail_update_Customer_invalid_Gender()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();
            model.Gender = (GenderType)99;

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.Birthdate,
                                                    model.Gender);

            var dbEntity = _mapper.Map<Customer>(model);
            _repository.GetById(model.Id).Returns(dbEntity);

            //Act
            await _appService.UpdateAsync(model);

            //Assert
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Sorry, the Gender informed is invalid."));
        }

        [Fact(DisplayName = "Should fail Update Customer (register not founf)")]
        public async Task Should_fail_update_Customer_not_found()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();

            var command = new UpdateCustomerCommand(model.Id,
                                                    model.FirstName,
                                                    model.LastName,
                                                    model.Birthdate,
                                                    model.Gender);

            _repository.GetById(model.Id).ReturnsNull();

            //Act
            await _appService.UpdateAsync(model);


            //Assert
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn =>
                    dn.Value == "Customer not found."));
        }

        [Fact(DisplayName = "Should Remove Customer")]
        public async Task Should_remove_Costumer()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();

            var command = new RemoveCustomerCommand(model.Id);

            _repository.GetById(model.Id).Returns(_mapper.Map<Customer>(model));

            //Act
            await _appService.RemoveAsync(model.Id);


            //Assert
            await _mediatorHandler
                .Received(1)
                .SendCommand(Arg.Is<RemoveCustomerCommand>(d => d.Id == model.Id));

        }

        [Fact(DisplayName = "Should fail Remove Customer (empty Id)")]
        public async Task Should_fail_remove_Costumer_empty_Id()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.Empty;

            var command = new RemoveCustomerCommand(model.Id);

            //Act
            await _appService.RemoveAsync(model.Id);


            //Assert
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn => dn.Value == "Please, must inform the Id."));
        }

        [Fact(DisplayName = "Should fail Remove Customer (not found)")]
        public async Task Should_fail_remove_Costumer_not_found()
        {
            //Arrange
            var model = new CustomerBuilder().BuildCustomer();
            model.Id = Guid.NewGuid();

            var command = new RemoveCustomerCommand(model.Id);

            _repository.GetById(model.Id).ReturnsNull();

            //Act
            await _appService.RemoveAsync(model.Id);


            //Assert
            await _mediatorHandler
                .Received(1)
                .PublishDomainNotification(Arg.Is<DomainNotification>(dn => dn.Value == "Customer not found."));
        }
    }
}
