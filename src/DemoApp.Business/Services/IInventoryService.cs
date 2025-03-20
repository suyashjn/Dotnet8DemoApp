using DemoApp.Models.Dtos;
using Microsoft.AspNetCore.Http;

namespace DemoApp.Business.Services
{
    public interface IInventoryService
    {
        Task SaveDataFromCsv(IFormFile file);
        Task<IEnumerable<InventoryItemDto>> GetInventory();
    }
}
