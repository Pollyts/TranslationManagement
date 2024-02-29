using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Controllers;
using TranslationManagement.Api.Extentions;
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
            try
            {
                var dbObjects = _service.GetAll();
                List<TranslatorResponseViewModel> viewModels = _mapper.Map<IEnumerable<Translator>, List<TranslatorResponseViewModel>>(dbObjects);
                return JsonResult(200, viewModels);
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }
        }

        //[HttpGet("{id}")]
        //public ActionResult Get(int id)
        //{
        //    return new JsonResult(_service.Get(id));
        //}

        [HttpGet("[action]")]
        public ActionResult GetByName(string name)
        {
            try
            {
                var dbObjects = _translatorService.GetByName(name);
                List<TranslatorResponseViewModel> viewModels = _mapper.Map<IEnumerable<Translator>, List<TranslatorResponseViewModel>>(dbObjects);
                return JsonResult(200, viewModels);
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }
        }

        [HttpPost]
        public ActionResult Create(TranslatorRequestViewModel model)
        {
            try
            {
                var create = _mapper.Map<Translator>(model);
                return JsonResult(201, _service.Create(create));
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }            
        }

        [HttpPut]
        public ActionResult Update(TranslatorRequestViewModel model)
        {
            try
            {
                var edit = _mapper.Map<Translator>(model);
                _service.Update(edit);
                return JsonResult(204, null);
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }
        }

        [HttpPut("[action]")]
        public ActionResult UpdateStatus(int translatorId, TranslatorStatus status)
        {
            try
            {
                _translatorService.UpdateStatus(translatorId, status);
                return JsonResult(204, null);
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }
        }

        [HttpPost("[action]")]
        public ActionResult AssignTranslator(int translatorId, int jobId)
        {
            try
            {
                _translatorService.AssignTranslator(translatorId, jobId);
                return JsonResult(200, null);
            }
            catch (Exception e)
            {
                return JsonErrorResult(e);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return JsonResult(200, null);
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
                    StatusCode = 400,
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