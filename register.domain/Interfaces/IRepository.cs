using register.domain.Entities;
using System.Linq;

namespace register.domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> Query();
    }
}
