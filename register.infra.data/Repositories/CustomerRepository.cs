using Microsoft.EntityFrameworkCore;
using register.domain.Entities;
using register.domain.Interfaces;
using register.infra.data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace register.infra.data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly RegisterDbContext _context;

        public CustomerRepository(RegisterDbContext context)
        {
            _context = context;
        }

        public void Add(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();
        }

        public List<Customer> GetAll()
        {
            return _context.Customer.AsNoTracking().ToList();
        }

        public Customer GetById(Guid id)
        {
            return _context.Customer.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Remove(Customer customer)
        {
            customer.DeleteDate = DateTime.Now;

            _context.Customer.Update(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            _context.Customer.Update(customer);
            _context.SaveChanges();
        }
    }
}
