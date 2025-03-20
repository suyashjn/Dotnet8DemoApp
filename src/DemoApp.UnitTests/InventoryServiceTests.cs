using AutoMapper;
using DemoApp.Business.Helpers;
using DemoApp.Business.Services;
using DemoApp.Data;
using DemoApp.Models.CsvFiles;
using DemoApp.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DemoApp.UnitTests.Services
{
    public class InventoryServiceTests
    {
        private readonly InventoryService _inventoryService;
        private readonly Mock<ICsvHelperService> _mockCsvHelperService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<InventoryService>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;

        public InventoryServiceTests()
        {
            _mockCsvHelperService = new Mock<ICsvHelperService>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<InventoryService>>();
            _mockMapper = new Mock<IMapper>();
            _inventoryService = new InventoryService(_mockCsvHelperService.Object, _mockUnitOfWork.Object, 
                _mockLogger.Object, _mockMapper.Object);
        }

        [Theory]
        [InlineData("file1.csv")]
        [InlineData("file2.csv")]
        public async Task SaveDataFromCsv_ShouldCompleteSuccessfully(string fileName)
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns(fileName);
            var inventoryItems = new List<InventoryItem> { new InventoryItem { /* Initialize properties */ } };
            var inventoryEntities = new List<Data.Entities.InventoryItem> { new Data.Entities.InventoryItem { /* Initialize properties */ } };

            _mockCsvHelperService.Setup(service => service.ReadCSV<InventoryItem>(It.IsAny<Stream>())).Returns(inventoryItems);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<Data.Entities.InventoryItem>>(inventoryItems)).Returns(inventoryEntities);
            _mockUnitOfWork.Setup(uow => uow.Inventory.AddRange(inventoryEntities));
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _inventoryService.SaveDataFromCsv(fileMock.Object);

            // Assert
            _mockCsvHelperService.Verify(service => service.ReadCSV<InventoryItem>(It.IsAny<Stream>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IEnumerable<Data.Entities.InventoryItem>>(inventoryItems), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Inventory.AddRange(inventoryEntities), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetInventoryData))]
        public async Task GetInventory_ShouldReturnListOfInventory(List<Data.Entities.InventoryItem> inventoryEntities, List<InventoryItemDto> expectedInventory)
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Inventory.GetAllAsync()).ReturnsAsync(inventoryEntities);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<InventoryItemDto>>(inventoryEntities)).Returns(expectedInventory);

            // Act
            var result = await _inventoryService.GetInventory();

            // Assert
            Assert.Equal(expectedInventory, result);
        }

        public static IEnumerable<object[]> GetInventoryData()
        {
            yield return new object[]
            {
                new List<Data.Entities.InventoryItem>
                {
                    new Data.Entities.InventoryItem { /* Initialize properties */ },
                    new Data.Entities.InventoryItem { /* Initialize properties */ }
                },
                new List<InventoryItemDto>
                {
                    new InventoryItemDto { /* Initialize properties */ },
                    new InventoryItemDto { /* Initialize properties */ }
                }
            };
        }
    }
}
