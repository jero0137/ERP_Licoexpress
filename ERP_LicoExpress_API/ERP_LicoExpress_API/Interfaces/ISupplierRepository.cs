
using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface ISupplierRepository
    {

        public Task<IEnumerable<Supplier>> GetAllAsync();
        public Task<Supplier> GetByIdAsync(int id);
        public Task<bool> DeleteAsync(Supplier unSupplier);
        public Task<bool> CreateAsync(Supplier unSupplier);

    }
}
