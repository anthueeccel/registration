using MediatR;
using register.domain.Commands;
using register.domain.Entities;
using register.domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace register.domain.CommandHandler
{
    public class CustomerCommandHandler : IRequestHandler<AddCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _repository;

        public CustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            _repository.Add(
                new Customer(Guid.NewGuid(),
                             request.FirstName,
                             request.LastName,
                             request.BirthDate,
                             request.Gender.Value));

            return Unit.Value;
        }
    }
}
