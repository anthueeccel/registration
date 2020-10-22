using AutoMapper;
using register.app.Interfaces;
using register.app.ViewModels;
using register.domain.Commands;
using register.domain.Interfaces;
using System.Threading.Tasks;

namespace register.app.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _repository;
        private readonly IMediatorHandler _mediatorHandler;

        public CustomerAppService(ICustomerRepository repository,
                                  IMapper mapper,
                                  IMediatorHandler mediatorHandler)
        {
            _repository = repository;
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        public async Task AddAsync(CustomerViewModel customerViewModel)
        {
            var command = new AddCustomerCommand(customerViewModel.FirstName,
                                                 customerViewModel.LastName,
                                                 customerViewModel.BirthDate,
                                                 customerViewModel.Gender);

            await _mediatorHandler.SendCommand(command);
        }
    }
}
