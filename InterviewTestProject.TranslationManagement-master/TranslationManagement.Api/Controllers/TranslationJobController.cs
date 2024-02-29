using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using External.ThirdParty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Controlers;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Services.Implementation;
using TranslationManagement.Api.Services.Interfaces;
using TranslationManagement.Api.ViewModels;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TranslationJobController : ControllerBase //: BaseController<TranslationJob>
    {
        IBaseService<TranslationJob> _service;
        ITranslationJobService _translationJobService;

        public TranslationJobController(
            IBaseService<TranslationJob> service,
            ITranslationJobService translationJobService)
        //: base(service)
        {
            _service = service;
            _translationJobService = translationJobService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return new JsonResult(_service.GetAll());
        }

        [HttpPost]
        public ActionResult Create(TranslationJob model)
        {
            return new JsonResult(_service.Create(model));
        }

        [HttpPost("[action]")]
        public ActionResult CreateJobWithFile(IFormFile file, string customerName = "")
        {
            return new JsonResult(_translationJobService.CreateJobWithFile(file, customerName));
        }

        [HttpPut("[action]")]
        public ActionResult UpdateJobStatus(int jobId, int translatorId, JobStatus status)
        {
            _translationJobService.UpdateJobStatus(jobId, status);
            return Ok();
        }
    }
}