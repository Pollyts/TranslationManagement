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
using TranslationManagement.Api.Extentions;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Services.Implementation;
using TranslationManagement.Api.Services.Interfaces;
using TranslationManagement.Api.ViewModels;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TranslationJobController : ControllerBase //, BaseController<TranslationJob>
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
            try
            {
                var dbObjects = _service.GetAll().ToList();
                List<TranslationJobResponseViewModel> viewModels = _mapper.Map<List<TranslationJob>, List<TranslationJobResponseViewModel>>(dbObjects);
                return JsonResult(200, viewModels);
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }
        }

        [HttpPost]
        public ActionResult Create(TranslationJobRequestViewModel model)
        {
            try
            {
                var create = _mapper.Map<TranslationJob>(model);
                return JsonResult(201, _service.Create(create));
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }
        }

        [HttpPost("[action]")]
        public ActionResult CreateWithFile(IFormFile file, string customerName = "")
        {
            try
            {
                _translationJobService.CreateJobWithFile(file, customerName);
                return JsonResult(201, null);
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }
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
            try
            {
                return JsonResult(200, _service.Get(jobId).OriginalContent);
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }
        }

        [HttpGet("[action]")]
        public ActionResult GetTranslatedContentFile(int jobId)
        {
            try
            {
                return JsonResult(200, _service.Get(jobId).TranslatedContent);
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }
        }

        [HttpPut("[action]")]
        public ActionResult UpdateJobStatus(int jobId, JobStatus status)
        {
            try
            {
                _translationJobService.UpdateJobStatus(jobId, status);
                return JsonResult(204, null);
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }
        }

        private JsonResult JsonResult(int code, object value)
        {
            return new JsonResult(value)
            {
                StatusCode = code
            };
        }

        private JsonResult JsonErrorResult(Exception e)
        {
            if (e is ClientException)
            {
                return new JsonResult(e.Message)
                {
                    StatusCode= 400,
                };
            }
            else
            {
                return new JsonResult(e.Message)
                {
                    StatusCode = 500,
                };
            }
        }
    }
}