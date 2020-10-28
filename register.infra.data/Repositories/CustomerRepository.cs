using register.domain.Entities;
using register.domain.Enum;
using register.domain.Interfaces;
using System;

namespace register.infra.data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Add(Customer costumer)
        {

        }

        public Customer GetById(Guid id)
        {
            //for development
            return new Customer(Guid.Parse("1ed5e347-bd10-411f-89fc-fe7a13149087"), "John", "Doe", DateTime.Parse("2000-04-21"), GenderType.Male);
        }
    }
}
