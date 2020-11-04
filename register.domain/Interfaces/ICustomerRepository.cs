using register.domain.Entities;
using System;
using System.Collections.Generic;

namespace register.domain.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        Customer GetById(Guid id);
        List<Customer> GetAll();
        void Remove(Customer customer);
        void Update(Customer customer);
    }
}
