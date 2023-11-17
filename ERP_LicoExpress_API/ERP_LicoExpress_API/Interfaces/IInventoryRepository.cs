using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface IInventoryRepository
    {
        public Task<IEnumerable<InventoryDetailed>> GetByLocationAsync(int id);
        public Task<IEnumerable<InventoryDetailed>> GetByLocationProductAsync(int location_id, int product_id);
        public Task<IEnumerable<InventoryDetailed>> GetByProductId(int producto_id);
        public Task<IEnumerable<CantProductLocation>> GetCantProductByLocationId(int location_id);
        public Task<bool> CreateAsync(int location_id, Inventory unInventory);
        public Task<Inventory> GetByIdAsync(int id);
        public Task<bool> UpdateAsync(int inventory_id, int location_id, Inventory unInventory);
    }
}
