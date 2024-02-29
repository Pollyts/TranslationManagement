using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Controllers;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Services.Interfaces;
using TranslationManagement.Api.ViewModels;

namespace TranslationManagement.Api.Controlers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslatorManagementController : ControllerBase //: BaseController<Translator>
    {
        IBaseService<Translator> _service;
        ITranslatorService _translatorService;
        private readonly IMapper _mapper;

        public TranslatorManagementController(IBaseService<Translator> service,
            IMapper mapper,
            ITranslatorService translatorService)
            //:base(service)
        {
            _service = service;
            _translatorService = translatorService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return new JsonResult(_service.GetAll());
        }

        //[HttpGet("{id}")]
        //public ActionResult Get(int id)
        //{
        //    return new JsonResult(_service.Get(id));
        //}

        [HttpGet("[action]")]
        public ActionResult GetByName(string name)
        {
            return new JsonResult(_translatorService.GetByName(name));
        }

        [HttpPost]
        public ActionResult Create(TranslatorRequestViewModel model)
        {
            var create = _mapper.Map<Translator>(model);
            return new JsonResult(_service.Create(create));
        }

        [HttpPut]
        public ActionResult Update(TranslatorRequestViewModel model)
        {
            var edit = _mapper.Map<Translator>(model);
            _service.Update(edit);
            return Ok();
        }

        [HttpPut("[action]")]
        public ActionResult UpdateStatus(int translatorId, TranslatorStatus status)
        {
            _translatorService.UpdateStatus(translatorId, status);
            return Ok();
        }

        [HttpPost("[action]")]
        public ActionResult AssignTranslator(int translatorId, int jobId)
        {
            _translatorService.AssignTranslator(translatorId, jobId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}