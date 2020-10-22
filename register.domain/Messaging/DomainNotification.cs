using MediatR;

namespace register.domain.Messaging
{
    public class DomainNotification : INotification
    {
        public string Value { get; }

        public DomainNotification(string value)
        {
            Value = value;
        }
    }
}
