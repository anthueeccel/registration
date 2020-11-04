using Microsoft.EntityFrameworkCore;
using register.domain.Entities;
using register.domain.Interfaces;
using register.infra.data.Context;
using System;
using System.Linq;

namespace register.infra.data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected RegisterDbContext _context;

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>().Where(c => c.DeleteDate == null).AsNoTracking();
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public TEntity GetById(Guid id)
        {
            return _context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Remove(TEntity entity)
        {
            entity.DeleteDate = DateTime.Now;

            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }
    }
}
