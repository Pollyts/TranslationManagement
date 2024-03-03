using Castle.Core.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using System.Xml.Linq;
using TranslationManagement.Api.Controlers;
using TranslationManagement.Api.Extentions;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Repositories;
using TranslationManagement.Api.Repositories.Interfaces;
using TranslationManagement.Api.Services.Interfaces;

namespace TranslationManagement.Api.Services.Implementation
{
    public class TranslationJobService : IBaseService<TranslationJob>, ITranslationJobService
    {
        private readonly ILogger<TranslationJobService> _logger;
        protected readonly IBaseRepository<TranslationJob> _repository;
        const double PricePerCharacter = 0.01;
        public TranslationJobService(IBaseRepository<TranslationJob> repository,
            ILogger<TranslationJobService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public virtual TranslationJob Get(int id)
        {
            var entity = _repository.FirstOrDefault(entity => entity.Id == id);
            if (entity == null)
                throw new ClientException("Item not fount");

            return entity;
        }

        public virtual IEnumerable<TranslationJob> GetAll()
        {
            return _repository.GetQuery().AsEnumerable();
        }

        public int Create(TranslationJob create)
        {
            create.Status = JobStatus.New;
            create.Price = create.OriginalContent.Length * PricePerCharacter;
            var item = _repository.Add(create);
            _repository.SaveChanges();
            return item.Id;

            //if (success)
            //{
            //    var notificationSvc = new UnreliableNotificationService();
            //    while (!notificationSvc.SendNotification("Job created: " + job.Id).Result)
            //    {
            //    }

            //    _logger.LogInformation("New job notification sent");
            //}
        }

        private TranslationJob ParseTxt(IFormFile file)
        {
            var reader = new StreamReader(file.OpenReadStream());
            return new TranslationJob()
            {
                OriginalContent = reader.ReadToEnd()
            };
        }
        private TranslationJob ParseXML(IFormFile file)
        {
            var reader = new StreamReader(file.OpenReadStream());
            var xdoc = XDocument.Parse(reader.ReadToEnd());
            return new TranslationJob()
            {
                OriginalContent = xdoc.Root.Element("Content").Value,
                CustomerName = xdoc.Root.Element("Customer").Value.Trim()
            };
        }

        public void Update(TranslationJob edit)
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

        public int CreateJobWithFile(IFormFile file, string customerName = "")
        {
            TranslationJob create = null;
            switch (file.FileName.Split(".").Last())
            {
                case ".txt":
                    create = ParseTxt(file);
                    create.CustomerName = customerName;
                    break;
                case ".xml":
                    create = ParseXML(file);
                    break;
                default:
                    throw new ClientException("Unsupported file");

            };

            return Create(create);
        }

        public void UpdateJobStatus(int jobId, JobStatus status)
        {
            var job = Get(jobId);

            _logger.LogInformation("Job status update request received: " + status + " for job " + jobId.ToString() + " by translator " + job.Translator?.Name);                                  

            if (job.Status == JobStatus.New && status == JobStatus.Completed 
                || job.Status == JobStatus.Completed
                || status == JobStatus.New
                || job.Status == status)
            {
                throw new ClientException("Can`t change status: the work is already in this status or completed");
            }

            job.Status = status;
            _repository.SaveChanges();
        }
    }
}
