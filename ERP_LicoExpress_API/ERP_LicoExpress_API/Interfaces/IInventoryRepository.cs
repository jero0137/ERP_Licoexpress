using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface IInventoryRepository
    {
        public Task<Inventory> GetByLocationAsync(int id);

    }
}
