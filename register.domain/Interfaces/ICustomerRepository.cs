using register.domain.Entities;
using System;

namespace register.domain.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer costumer);
        Customer GetById(Guid id);
    }
}
