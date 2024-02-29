using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public TranslationJobController(
            IBaseService<TranslationJob> service,
            IMapper mapper,
            ITranslationJobService translationJobService)
        //: base(service)
        {
            _service = service;
            _translationJobService = translationJobService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var dbObjects = _service.GetAll();
            List<TranslationJobResponseViewModel> viewModels = _mapper.Map<IEnumerable<TranslationJob>, List<TranslationJobResponseViewModel>>(dbObjects);
            return new JsonResult(viewModels);
        }

        [HttpPost]
        public ActionResult Create(TranslationJobRequestViewModel model)
        {
            var create = _mapper.Map<TranslationJob>(model);
            return new JsonResult(_service.Create(create));
        }

        [HttpPost("[action]")]
        public ActionResult CreateWithFile(IFormFile file, string customerName = "")
        {
            return new JsonResult(_translationJobService.CreateJobWithFile(file, customerName));
        }

        /// <summary>
        /// if we want to store our files as files
        /// </summary>
        /// <param name="id">Job id</param>
        /// <returns></returns>
        //[HttpGet("OriginalFile/{id}")]
        //public virtual ActionResult GetOriginalFile(int jobId)
        //{
        //    var file = _service.GetFile(id);
        //    if (file == null)
        //        return StatusCode(500);

        //    var stream = new MemoryStream(file.Data);

        //    return File(stream, file.MimeType, file.Name);
        //}

        [HttpGet("[action]")]
        public ActionResult GetOriginalContentFile(int jobId)
        {
            return new JsonResult(_service.Get(jobId).OriginalContent);
        }

        [HttpGet("[action]")]
        public ActionResult GetTranslatedContentFile(int jobId)
        {
            return new JsonResult(_service.Get(jobId).TranslatedContent);
        }

        [HttpPut("[action]")]
        public ActionResult UpdateJobStatus(int jobId, int translatorId, JobStatus status)
        {
            _translationJobService.UpdateJobStatus(jobId, status);
            return Ok();
        }
    }
}