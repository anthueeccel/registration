using register.domain.Entities;
using System;
using System.Collections.Generic;

namespace register.domain.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer costumer);
        Customer GetById(Guid id);
        List<Customer> GetAll();
        void Remove(Guid id);
        void Update(Customer customer);
    }
}
