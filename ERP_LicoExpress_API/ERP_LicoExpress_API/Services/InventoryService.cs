using ERP_LicoExpress_API.Helpers;
using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Repositories;

namespace ERP_LicoExpress_API.Services
{
    public class InventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Inventory> GetByLocationAsync(int location_id)
        {
            var unInventory= await _inventoryRepository
                .GetByLocationAsync(location_id);

            if (unInventory.Id == 0)
                throw new AppValidationException($"Inventario no encontrado para la sede {location_id}");

            return unInventory;
        }

        
    }
}
