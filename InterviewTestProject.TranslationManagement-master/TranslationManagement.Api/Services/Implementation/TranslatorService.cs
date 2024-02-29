using System.Collections.Generic;
using System.Linq;
using System;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Repositories.Interfaces;
using TranslationManagement.Api.Services.Interfaces;

namespace TranslationManagement.Api.Services.Implementation
{
    public class TranslatorService : IBaseService<Translator> , ITranslatorService
    {
        protected readonly IBaseRepository<Translator> _repository;
        protected readonly IBaseRepository<TranslationJob> _repositoryJob;

        public TranslatorService(IBaseRepository<Translator> repository, IBaseRepository<TranslationJob> repositoryJob)
        {
            _repository = repository;
            _repositoryJob = repositoryJob;
        }

        public virtual Translator Get(int id)
        {
            var entity = _repository.FirstOrDefault(entity => entity.Id == id);
            if (entity == null)
                throw new Exception("Data not found");

            return entity;
        }

        public virtual IEnumerable<Translator> GetAll()
        {
            return _repository.GetQuery().AsEnumerable();
        }

        public int Create(Translator create)
        {
            var item = _repository.Add(create);
            _repository.SaveChanges();
            return item.Id;
        }

        public void Update(Translator edit)
        {
            _repository.Update(edit);
            _repository.SaveChanges();
        }

        public void Delete(int id)
        {
            var translator = _repository.FirstOrDefault(t => t.Id == id);
            _repository.Delete(translator);
            _repository.SaveChanges();
        }

        public IEnumerable<Translator> GetByName(string name)
        {
            return _repository.GetQuery().Where(t => t.Name == name).AsEnumerable();
        }

        public void UpdateStatus(int translatorId, TranslatorStatus status)
        {
            //_logger.LogInformation("User status update request: " + newStatus + " for user " + Translator.ToString());
            //if (TranslatorStatuses.Where(status => status == newStatus).Count() == 0)
            //{
            //    throw new ArgumentException("unknown status");
            //}
            var entity = Get(translatorId);
            entity.Status = status;
            _repository.SaveChanges();
        }

        public void AssignTranslator(int translatorId, int jobId)
        {
            var translator = Get(translatorId);
            if(translator.Status != TranslatorStatus.Certified)
            {
                throw new Exception("Only Certified translators can work on jobs");
            }

            var job = _repositoryJob.FirstOrDefault(j=>j.Id == jobId);
            if(job != null)
            {
                job.TranslatorId = translatorId;
                _repositoryJob.Update(job);
                _repositoryJob.SaveChanges();
            }
            else
            {
                throw new Exception("Job not found");
            }
            
        }
    }
}

