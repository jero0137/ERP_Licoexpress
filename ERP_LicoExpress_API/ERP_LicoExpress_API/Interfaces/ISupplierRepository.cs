
using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface ISupplierRepository
    {

        public Task<IEnumerable<Supplier>> GetAllAsync();
    }
}
