using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface IInventoryRepository
    {
        public Task<IEnumerable<InventoryDetailed>> GetByLocationAsync(int id);
        public Task<IEnumerable<InventoryDetailed>> GetByLocationProductAsync(int location_id, int product_id);
        public Task<bool> CreateAsync(Inventory unInventory);
    }
}
