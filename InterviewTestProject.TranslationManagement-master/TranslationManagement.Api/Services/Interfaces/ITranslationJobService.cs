using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.Services.Interfaces
{
    public interface ITranslationJobService
    {
        int CreateJobWithFile(IFormFile file, string customerName);
        void UpdateJobStatus(int jobId, JobStatus status);
        //IEnumerable<TranslationJob> GetAll();
        //TranslationJob Get(int id);
        //int Create(TranslationJob create);
        //void Update(TranslationJob edit);
        //void Delete(int id);
    }
}
