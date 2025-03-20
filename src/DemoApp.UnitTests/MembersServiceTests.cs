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
    public class MembersServiceTests
    {
        private readonly MembersService _membersService;
        private readonly Mock<ICsvHelperService> _mockCsvHelperService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<MembersService>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;

        public MembersServiceTests()
        {
            _mockCsvHelperService = new Mock<ICsvHelperService>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<MembersService>>();
            _mockMapper = new Mock<IMapper>();
            _membersService = new MembersService(_mockCsvHelperService.Object, _mockUnitOfWork.Object, 
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
            var members = new List<Member> { new Member { /* Initialize properties */ } };
            var memberEntities = new List<Data.Entities.Member> { new Data.Entities.Member { /* Initialize properties */ } };

            _mockCsvHelperService.Setup(service => service.ReadCSV<Member>(It.IsAny<Stream>())).Returns(members);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<Data.Entities.Member>>(members)).Returns(memberEntities);
            _mockUnitOfWork.Setup(uow => uow.Members.AddRange(memberEntities));
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _membersService.SaveDataFromCsv(fileMock.Object);

            // Assert
            _mockCsvHelperService.Verify(service => service.ReadCSV<Member>(It.IsAny<Stream>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IEnumerable<Data.Entities.Member>>(members), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Members.AddRange(memberEntities), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetMembersData))]
        public async Task GetMembers_ShouldReturnListOfMembers(List<Data.Entities.Member> memberEntities, List<MemberDto> expectedMembers)
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Members.GetAllAsync()).ReturnsAsync(memberEntities);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<MemberDto>>(memberEntities)).Returns(expectedMembers);

            // Act
            var result = await _membersService.GetMembers();

            // Assert
            Assert.Equal(expectedMembers, result);
        }

        public static IEnumerable<object[]> GetMembersData()
        {
            yield return new object[]
            {
                new List<Data.Entities.Member>
                {
                    new Data.Entities.Member { /* Initialize properties */ },
                    new Data.Entities.Member { /* Initialize properties */ }
                },
                new List<MemberDto>
                {
                    new MemberDto { /* Initialize properties */ },
                    new MemberDto { /* Initialize properties */ }
                }
            };
        }
    }
}
