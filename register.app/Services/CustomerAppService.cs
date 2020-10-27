using AutoMapper;
using register.app.Interfaces;
using register.app.ViewModels;
using register.domain.Commands;
using register.domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace register.app.Services
{
    public class CustomerAppService : AppServiceBase, ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _repository;

        public CustomerAppService(ICustomerRepository repository,
                                  IMapper mapper,
                                  IMediatorHandler mediatorHandler) 
            :base(mediatorHandler)

        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(CustomerViewModel customerViewModel)
        {
            var command = new AddCustomerCommand(customerViewModel.FirstName,
                                                 customerViewModel.LastName,
                                                 customerViewModel.BirthDate,
                                                 customerViewModel.Gender);

            if (!command.IsValid())
            {
                await RaiseCommandValidationErrors(command);
                return;
            }

            await _mediatorHandler.SendCommand(command);
        }
    }
}
