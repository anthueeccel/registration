using register.domain.Entities;
using register.domain.Interfaces;
using register.infra.data.Context;

namespace register.infra.data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(RegisterDbContext context)
        {
            _context = context;
        }
    }
}
