using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Repositories;
using GestionTransporte_CS_API_PostgresSQL.Helpers;

namespace ERP_LicoExpress_API.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository
                .GetAllAsync();
        }


        public async Task<Product> GetByIdAsync(int product_id)
        {
            var unProduct = await _productRepository
                .GetByIdAsync(product_id);

            if (unProduct.Id == 0)
                throw new AppValidationException($"Producto no encontrado con el id {product_id}");

            return unProduct;
        }


        public async Task DeleteAsync(int product_id)
        {
            var productExistente = await _productRepository.GetByIdAsync(product_id);

            if (productExistente.Id == 0)
                throw new AppValidationException($"No existe un producto con el Id {product_id} que se pueda eliminar");

            try
            {
                bool resultadoAccion = await _productRepository
                    .DeleteAsync(productExistente);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");
            }
            catch (DbOperationException error)
            {
                throw error;
            }
        }


        public async Task<Product> CreateAsync(Product unProduct)
        {
            if (unProduct.Nombre.Length == 0)
                throw new AppValidationException("No se puede insertar un producto sin un nombre");

            if (unProduct.Tipo_id == 0)
                throw new AppValidationException("No se puede insertar un producto sin un tipo");

            if (unProduct.Tamaño.Length == 0)
                throw new AppValidationException("No se puede insertar un producto sin un tamaño");

            if (unProduct.Imagen.Length == 0)
                throw new AppValidationException("No se puede insertar un producto sin una imagen");

            if (unProduct.Precio_base == 0)
                throw new AppValidationException("No se puede insertar un producto sin un precio base");

            if (unProduct.Precio_venta == 0)
                throw new AppValidationException("No se puede insertar un producto sin un precio venta");

            if (unProduct.Proveedor_id == 0)
                throw new AppValidationException("No se puede insertar un producto sin un proveedor");



            try
            {
                bool resultadoAccion = await _productRepository.CreateAsync(unProduct);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                var productExistente = await _productRepository.GetByIdAsync(unProduct.Id);

                return productExistente;

            }
            catch (DbOperationException error)
            {
                throw error;
            }

        }
    }
}
