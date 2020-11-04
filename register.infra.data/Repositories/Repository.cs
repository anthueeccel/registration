using Microsoft.EntityFrameworkCore;
using register.domain.Entities;
using register.domain.Interfaces;
using register.infra.data.Context;
using System.Linq;

namespace register.infra.data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected RegisterDbContext _context;

        public IQueryable<TEntity> Query()
        {
            return _context
                       .Set<TEntity>()
                       .Where(c => c.DeleteDate == null)
                       .AsNoTracking();
        }
    }
}
