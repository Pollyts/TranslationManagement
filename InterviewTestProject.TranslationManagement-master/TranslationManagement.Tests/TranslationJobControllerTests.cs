using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationManagement.Api.Controllers;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Services.Interfaces;
using TranslationManagement.Api.ViewModels;
using Xunit;
using Moq;
using TranslationManagement.Api;

namespace TranslationManagement.Tests
{
    public class TranslationJobControllerTests
    {
        private readonly TranslationJobController _controller;
        private readonly Mock<IBaseService<TranslationJob>> _mockBaseService;
        private readonly Mock<ITranslationJobService> _mockTranslationJobService;
        private readonly IMapper _mapper;

        public TranslationJobControllerTests()
        {
            //configure Mapper
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = new Mapper(configuration);

            _mockBaseService = new Mock<IBaseService<TranslationJob>>();
            _mockTranslationJobService = new Mock<ITranslationJobService>();
            _controller = new TranslationJobController(_mockBaseService.Object, _mapper, _mockTranslationJobService.Object);
        }

        

        [Fact]
        public void Get_ReturnsOkResult_WithListOfTranslationJobResponseViewModel()
        {
            // Arrange
            _mockBaseService.Setup(s=>s.GetAll()).Returns(GetTestJobs());

            //Act
            var result = _controller.Get();

            //Asset
            Assert.NotNull(result);
            var okResult = Assert.IsType<JsonResult>(result);            
            Assert.Equal(200, okResult.StatusCode);
            var viewModelResult = Assert.IsType<List<TranslationJobResponseViewModel>>(okResult.Value);
            Assert.Equal(viewModelResult.Count(), GetTestJobs().Count());
        }

        [Fact]
        public void Get_OriginalContentFile()
        {
            // Arrange
            _mockBaseService.Setup(s => s.Get(123)).Returns(GetTestJobs().First());

            //Act
            var result = _controller.GetOriginalContentFile(123);

            //Asset
            var okResult = Assert.IsType<JsonResult>(result);
            var viewModelResult = Assert.IsType<string>(okResult.Value);
            Assert.Equal(viewModelResult, GetTestJobs().First().OriginalContent);
        }

        private List<TranslationJob> GetTestJobs()
        {
            var jobs = new List<TranslationJob>
            {
                new TranslationJob()
                {
                    Id = 123,
                    CustomerName= "Polina",
                    Price = 10.0,
                    Status = JobStatus.InProgress,
                    OriginalContent = "original content Polina",
                    Translator = new Translator()
                    {
                        Id = 1,
                        Name = "Test"
                    }
                },
                new TranslationJob()
                {
                    Id = 456,
                    CustomerName= "Mark",
                    Price = 3.0,
                    Status = JobStatus.Completed,
                    OriginalContent = "original content Mark",
                    TranslatedContent = "translated content Mark",
                    Translator = new Translator()
                    {
                        Id = 1,
                        Name = "Test"
                    }
                },
                new TranslationJob()
                {
                    Id = 789,
                    CustomerName= "Alice",
                    Price = 3.9,
                    Status = JobStatus.New,
                    OriginalContent = "original content Alice",
                    Translator = new Translator()
                    {
                        Id = 1,
                        Name = "Test"
                    }

                }
            };
            return jobs;
        }
    }
}
