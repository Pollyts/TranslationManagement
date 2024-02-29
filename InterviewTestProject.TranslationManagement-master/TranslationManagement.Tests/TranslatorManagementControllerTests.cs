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
using TranslationManagement.Api.Controlers;

namespace TranslationManagement.Tests
{
    public class TranslatorManagementControllerTests
    {
        private readonly TranslatorManagementController _controller;
        private readonly Mock<IBaseService<Translator>> _mockBaseService;
        private readonly Mock<ITranslatorService> _mockTranslatorService;
        private readonly IMapper _mapper;

        public TranslatorManagementControllerTests()
        {
            //configure Mapper
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = new Mapper(configuration);

            _mockBaseService = new Mock<IBaseService<Translator>>();
            _mockTranslatorService = new Mock<ITranslatorService>();
            _controller = new TranslatorManagementController(_mockBaseService.Object, _mapper, _mockTranslatorService.Object);
        }

        [Fact]
        public void Create_AddsNewTranslator()
        {
            // Arrange
            var translatorRequest = new TranslatorRequestViewModel { Name = "New Translator" };
            _mockBaseService.Setup(service => service.Create(It.IsAny<Translator>())).Returns(4); 

            // Act
            var result = _controller.Create(translatorRequest) as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(4, result.Value); 
        }

        [Fact]
        public void UpdateStatus_UpdatesTranslatorStatus()
        {
            // Arrange

            // Act
            var result = _controller.UpdateStatus(1, TranslatorStatus.Certified) as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
            _mockTranslatorService.Verify(service => service.UpdateStatus(1, TranslatorStatus.Certified), Times.Once);
        }
    }
}
