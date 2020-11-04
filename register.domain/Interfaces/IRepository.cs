using register.domain.Entities;
using System;
using System.Linq;

namespace register.domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> Query();
        void Add(TEntity entity);
        TEntity GetById(Guid id);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}
