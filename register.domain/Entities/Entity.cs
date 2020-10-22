using System;

namespace register.domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}
