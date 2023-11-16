using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface IInventoryRepository
    {
        public Task<IEnumerable<InventoryDetailed>> GetByLocationAsync(int id);

    }
}
