using System.Collections.Generic;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.Services.Interfaces
{
    public interface IBaseService<TEntity>
        where TEntity : class, IEntityDb
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        int Create(TEntity create);
        void Update(TEntity edit);
        void Delete(int id);
    }
}
