using register.app.ViewModels;
using System.Threading.Tasks;

namespace register.app.Interfaces
{
    public interface ICustomerAppService
    {
        Task AddAsync(CustomerViewModel customerViewModel);
        Task UpdateAsync(CustomerViewModel customerViewModel);
    }
}
