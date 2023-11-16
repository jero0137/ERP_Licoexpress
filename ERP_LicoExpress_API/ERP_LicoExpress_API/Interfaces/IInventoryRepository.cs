using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface IInventoryRepository
    {
        public Task<IEnumerable<Inventory>> GetByLocationAsync(int id);

    }
}
