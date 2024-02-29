using System.Security.Principal;
using System;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Repositories.Interfaces;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TranslationManagement.Api.Services.Interfaces;

namespace TranslationManagement.Api.Services.Implementation
{
    public class BaseService<TEntity>: IBaseService<TEntity>
        where TEntity : class, IEntityDb, new()
    {
        protected readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public int Create(TEntity create)
        {
            var item = _repository.Add(create);
            _repository.SaveChanges();
            return item.Id;
        }

        public void Delete(int id)
        {
            var entity = _repository.FirstOrDefault(t => t.Id == id);
            _repository.Delete(entity);
            _repository.SaveChanges();
        }

        public virtual TEntity Get(int id)
        {
            var entity = _repository.FirstOrDefault(entity => entity.Id == id);
            if (entity == null)
                throw new Exception("Данные не найдены");

            return entity;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetQuery().AsEnumerable();
        }

        public void Update(TEntity edit)
        {
            _repository.Update(edit);
            _repository.SaveChanges();
        }
    }
}
