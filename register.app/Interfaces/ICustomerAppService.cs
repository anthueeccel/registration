using register.app.ViewModels;
using register.domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace register.app.Interfaces
{
    public interface ICustomerAppService
    {
        Task AddAsync(CustomerViewModel customerViewModel);
        IEnumerable<Customer> GetAll();
        Task UpdateAsync(CustomerViewModel customerViewModel);
        Task RemoveAsync(Guid id);
        Customer GetById(Guid id);
    }
}
