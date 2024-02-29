using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TranslationManagement.Api.Extentions;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Repositories.Interfaces;

namespace TranslationManagement.Api.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IEntityDb, new()
    {
        private readonly Lazy<DbSet<TEntity>> _dbSet;
        protected readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = new Lazy<DbSet<TEntity>>(() => _dbContext.Set<TEntity>());
        }

        public virtual IQueryable<TEntity> GetQuery()
        {
            return _dbSet.Value.AsQueryable();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery().FirstOrDefault(predicate);
        }

        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ClientException("Item not fount");
            }

            return _dbSet.Value.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ClientException("Item not fount");
            }

            var edit = FirstOrDefault(e => e.Id == entity.Id);

            _dbContext.Entry(edit).CurrentValues.SetValues(entity);

            return edit;
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ClientException("Item not fount");
            }

            _dbSet.Value.Remove(entity);
        }        

        public virtual async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }
        }        
    }
}
