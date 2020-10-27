using register.domain.Commands;
using register.domain.Interfaces;
using register.domain.Messaging;
using System.Threading.Tasks;

namespace register.app.Services
{
    public abstract class AppServiceBase
    {
        protected readonly IMediatorHandler _mediatorHandler;

        protected AppServiceBase(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected async Task RaiseCommandValidationErrors(Command command)
        {
            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishDomainNotification(new DomainNotification(error.ErrorMessage));
            }
        }
    }
}
