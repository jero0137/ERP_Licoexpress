﻿using ERP_LicoExpress_API.Models;

namespace ERP_LicoExpress_API.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task<Product> GetByIdAsync(int id);
        public Task<bool> DeleteAsync(Product unProducto);
        public Task<bool> CreateAsync(Product unProducto);
    }
}
