using AutoMapper;
using DemoApp.Business.Helpers;
using DemoApp.Data;
using DemoApp.Models.CsvFiles;
using DemoApp.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DemoApp.Business.Services
{
    public class MembersService : IMembersService
    {
        private readonly ICsvHelperService _csvHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MembersService> _logger;
        private readonly IMapper _mapper;

        public MembersService(ICsvHelperService csvHelper, IUnitOfWork unitOfWork,
             ILogger<MembersService> logger, IMapper mapper)
        {
            _csvHelper = csvHelper ?? throw new ArgumentNullException(nameof(csvHelper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task SaveDataFromCsv(IFormFile file)
        {
            _logger.LogInformation("Parsing CSV file to get members data");
            var members = _csvHelper.ReadCSV<Member>(file.OpenReadStream());
            _unitOfWork.Members.AddRange(_mapper.Map<IEnumerable<Data.Entities.Member>>(members));
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Members data added to db successfully");
        }

        public async Task<IEnumerable<MemberDto>> GetMembers()
        {
            var members = await _unitOfWork.Members.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(members);
        }
    }
}
