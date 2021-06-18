using MediatR;
using register.domain.Commands;
using register.domain.Entities;
using register.domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace register.domain.CommandHandler
{
    public class CustomerCommandHandler : IRequestHandler<AddCustomerCommand, Unit>,
                                          IRequestHandler<UpdateCustomerCommand, Unit>,
                                          IRequestHandler<RemoveCustomerCommand, Unit>
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

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            
            _repository.Update(
               new Customer(request.Id,
                            request.FirstName,
                            request.LastName,
                            request.BirthDate,
                            request.Gender.Value));
            
            return Unit.Value;
        }

        public async Task<Unit> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            _repository.Remove(request.DbEntity);

            return Unit.Value;
        }
    }
}
