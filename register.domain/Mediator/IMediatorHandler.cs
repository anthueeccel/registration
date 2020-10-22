using register.domain.Commands;
using register.domain.Messaging;
using System.Threading.Tasks;

namespace register.domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task PublishDomainNotification<T>(T notification) where T : DomainNotification;
    }
}
