using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Services;
using ERP_LicoExpress_API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ERP_LicoExpress_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : Controller
    {
        private readonly InventoryService _inventoryService;
        public InventoriesController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }


        [HttpGet("{location_id:int}")]
        public async Task<IActionResult> GetByLocationAsync(int location_id)
        {
            try
            {
                var losInventory = await _inventoryService
                    .GetByLocationAsync(location_id);
                return Ok(losInventory);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

    }
}
