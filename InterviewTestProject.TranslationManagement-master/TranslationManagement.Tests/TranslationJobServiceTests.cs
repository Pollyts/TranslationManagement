using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationManagement.Api;
using TranslationManagement.Api.Controllers;
using TranslationManagement.Api.Extentions;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Repositories.Interfaces;
using TranslationManagement.Api.Services.Implementation;
using TranslationManagement.Api.Services.Interfaces;
using Xunit;

namespace TranslationManagement.Tests
{
    public class TranslationJobServiceTests
    {
        private readonly Mock<IBaseRepository<TranslationJob>> _mockRepository;
        private readonly Mock<ITranslationJobService> _mockTranslationJobService;
        private readonly Mock<ILogger<TranslationJobService>> _mockLogger;
        private readonly IMapper _mapper;

        public TranslationJobServiceTests()
        {
            //configure Mapper
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = new Mapper(configuration);

            _mockRepository = new Mock<IBaseRepository<TranslationJob>>();
            _mockLogger = new Mock<ILogger<TranslationJobService>>();
        }

        [Fact]
        public void Get_WithValidId_ShouldReturnEntity()
        {
            // Arrange
            int entityTestId = 1;
            var mockRepository = new Mock<IBaseRepository<TranslationJob>>();
            mockRepository.Setup(repo => repo.FirstOrDefault(x=>x.Id == entityTestId)).Returns(new TranslationJob { Id = entityTestId });
            var service = new TranslationJobService(mockRepository.Object, _mockLogger.Object);

            // Act
            var result = service.Get(entityTestId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entityTestId, result.Id);
        }

        [Fact]
        public void Get_WithInValidId_ShouldThrowClientException()
        {
            // Arrange
            int entityTestId = 0;
            var mockRepository = new Mock<IBaseRepository<TranslationJob>>();
            mockRepository.Setup(repo => repo.FirstOrDefault(x => x.Id == entityTestId)).Returns((TranslationJob)null);
            var service = new TranslationJobService(mockRepository.Object, _mockLogger.Object);

            // Act and Assert
            Assert.Throws<ClientException>(() => service.Get(entityTestId));
        }

        [Fact]
        public void CreateJobWithFile_WithUnsupportedFile_ShouldThrowClientException()
        {
            // Arrange
            var file = new Mock<IFormFile>();
            file.Setup(f => f.FileName).Returns("file.pdf");
            var service = new TranslationJobService(null, null);

            // Act and Assert
            Assert.Throws<ClientException>(() => service.CreateJobWithFile(file.Object));
        }
    }
}
