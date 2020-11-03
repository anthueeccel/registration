using register.domain.Entities;
using register.domain.Enum;
using register.domain.Interfaces;
using System;
using System.Collections.Generic;

namespace register.infra.data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Add(Customer costumer)
        {
            //TODO: add to database 
        }

        public List<Customer> GetAll()
        {
            //for development
            return new List<Customer>
            {
                (new Customer(Guid.Parse("1ed5e347-bd10-411f-89fc-fe7a13149087"), "John", "Stout", DateTime.Parse("2000-04-21"), GenderType.Male)),
                (new Customer(Guid.Parse("1ed5e347-bd10-411f-89fc-fe7a13149088"), "Mary", "Dunkel", DateTime.Parse("2005-02-12"), GenderType.Female)),
                (new Customer(Guid.Parse("1ed5e347-bd10-411f-89fc-fe7a13149089"), "Jane", "Pilsen", DateTime.Parse("2000-11-02"), GenderType.Male))
            };
        }

        public Customer GetById(Guid id)
        {
            //for development
            return new Customer(Guid.Parse("1ed5e347-bd10-411f-89fc-fe7a13149087"), "John", "Doe", DateTime.Parse("2000-04-21"), GenderType.Male);
        }

        public void Remove(Guid id)
        {
            //TODO: remove from database
        }

        public void Update(Customer customer)
        {
            //TODO update on database
        }
    }
}
