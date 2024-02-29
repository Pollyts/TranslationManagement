using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.ViewModels;

namespace TranslationManagement.Api.Services.Interfaces
{
    public interface ITranslationJobService
    {
        int CreateJobWithFile(IFormFile file, string customerName);
        void UpdateJobStatus(int jobId, JobStatus status);
    }
}
