using DemoApp.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DemoApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _InventoryService;

        public InventoryController(IInventoryService InventoryService)
        {
            _InventoryService = InventoryService ?? throw new ArgumentNullException(nameof(InventoryService));
        }

        [HttpPost]
        [EndpointSummary("Endpoint for uploading csv file contaning Inventorys info.")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("/uploadinventory")]
        public async Task<IActionResult> UploadInventory(IFormFile file)
        {
            await _InventoryService.SaveDataFromCsv(file);
            return Ok();
        }

        [HttpGet]
        [EndpointSummary("Endpoint for getting Inventorys.")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetInventorys()
        {
            var Inventorys = await _InventoryService.GetInventory();
            return Ok(Inventorys);
        }
    }
}
