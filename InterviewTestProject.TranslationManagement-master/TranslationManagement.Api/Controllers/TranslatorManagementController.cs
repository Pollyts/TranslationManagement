using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Controllers;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Services.Interfaces;

namespace TranslationManagement.Api.Controlers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslatorManagementController : ControllerBase //: BaseController<Translator>
    {
        IBaseService<Translator> _service;
        ITranslatorService _translatorService;

        public TranslatorManagementController(IBaseService<Translator> service,
            ITranslatorService translatorService)
            //:base(service)
        {
            _service = service;
            _translatorService = translatorService;
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
        public ActionResult Create(Translator create)
        {
            return new JsonResult(_service.Create(create));
        }

        [HttpPut]
        public ActionResult Update(Translator create)
        {
            _service.Update(create);
            return Ok();
        }

        [HttpPut("[action]")]
        public ActionResult UpdateStatus(int translatorId, TranslatorStatus status)
        {
            _translatorService.UpdateStatus(translatorId, status);
            return Ok();
        }
    }
}