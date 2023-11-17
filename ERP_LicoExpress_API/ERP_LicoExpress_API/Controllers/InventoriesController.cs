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

        [HttpGet("{location_id:int}/{product_id:int}")]
        public async Task<IActionResult> GetByLocationProductAsync(int location_id, int product_id)
        {
            try
            {
                var losInventory = await _inventoryService
                    .GetByLocationProductAsync(location_id, product_id);
                return Ok(losInventory);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }


        [HttpPost("{location_id:int}")]
        public async Task<IActionResult> CreateAsync(int location_id, Inventory unInventory)
        {
            try
            {
                var inventoryCreado = await _inventoryService
                    .CreateAsync(location_id,unInventory);

                return Ok(inventoryCreado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

        [HttpPut("{location_id:int}/{inventory_id:int}")]
        public async Task<IActionResult> UpdateAsync(int inventory_id, int location_id, Inventory unInventario)
        {
            try
            {
                var locationActualizada = await _inventoryService
                    .UpdateAsync(inventory_id, location_id, unInventario);

                return Ok(locationActualizada);

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

    }
}
