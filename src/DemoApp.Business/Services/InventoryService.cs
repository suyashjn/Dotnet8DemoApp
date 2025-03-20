using AutoMapper;
using DemoApp.Business.Helpers;
using DemoApp.Data;
using DemoApp.Models.CsvFiles;
using DemoApp.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DemoApp.Business.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ICsvHelperService _csvHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<InventoryService> _logger;
        private readonly IMapper _mapper;

        public InventoryService(ICsvHelperService csvHelper, IUnitOfWork unitOfWork,
             ILogger<InventoryService> logger, IMapper mapper)
        {
            _csvHelper = csvHelper ?? throw new ArgumentNullException(nameof(csvHelper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task SaveDataFromCsv(IFormFile file)
        {
            _logger.LogInformation("Parsing CSV file to get inventory data");
            var inventory = _csvHelper.ReadCSV<InventoryItem>(file.OpenReadStream());
            _unitOfWork.Inventory.AddRange(_mapper.Map<IEnumerable<Data.Entities.InventoryItem>>(inventory));
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("inventory data added to db successfully");
        }

        public async Task<IEnumerable<InventoryItemDto>> GetInventory()
        {
            var inventory = await _unitOfWork.Inventory.GetAllAsync();
            return _mapper.Map<IEnumerable<InventoryItemDto>>(inventory);
        }
    }
}
