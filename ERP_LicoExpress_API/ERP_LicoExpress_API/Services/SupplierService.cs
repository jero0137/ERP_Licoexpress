using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Services
{
    public class SupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _supplierRepository
                .GetAllAsync();
        }
    }
}
