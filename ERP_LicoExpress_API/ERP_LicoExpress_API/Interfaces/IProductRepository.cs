using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductDetailed>> GetAllAsync();
        public Task<Product> GetByIdAsync(int id);
        
        public Task<bool> DeleteAsync(Product unProducto);
        public Task<bool> CreateAsync(Product unProducto);
        public Task<bool> UpdateAsync(int product_id, Product unProducto);
    }
}
