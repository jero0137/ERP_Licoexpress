using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Repositories;
using ERP_LicoExpress_API.Helpers;

namespace ERP_LicoExpress_API.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public ProductService(IProductRepository productRepository, IInventoryRepository inventoryRepository)
        {
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<ProductDetailed>> GetAllAsync()
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

            var inventarios =  await _inventoryRepository.GetByProductId(product_id);

            if(inventarios.Count() > 0)
                throw new AppValidationException($"No se puede eliminar el producto {productExistente.Nombre} ya que hay inventarios que lo registran.");

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


        public async Task<Product> UpdateAsync(int producto_id, Product unProducto)
        {


            var productoExistente = await _productRepository
                .GetByIdAsync(producto_id);

            if (productoExistente.Id == 0)
                throw new AppValidationException($"No existe un producto registrado con el id {unProducto.Id}");

            if (producto_id != productoExistente.Id)
                throw new AppValidationException($"Inconsistencia en el Id de la sede a actualizar. Verifica argumentos");


            if (productoExistente.Tipo_id == 0)
                throw new AppValidationException("No se puede actualizar un tipo nulo");

            if (productoExistente.Proveedor_id == 0)
                throw new AppValidationException("No se puede actualizar un proveedor nulo");

            if (string.IsNullOrEmpty(productoExistente.Tamaño))
                throw new AppValidationException("No se puede actualizar un tamaño nulo");

            if (string.IsNullOrEmpty(productoExistente.Nombre))
                throw new AppValidationException("No se puede actualizar un nombre nulo");

            if (string.IsNullOrEmpty(productoExistente.Imagen))
                throw new AppValidationException("No se puede actualizar una imagen nula");

            if (productoExistente.Precio_base == 0)
                throw new AppValidationException("No se puede actualizar un precio base nulo");

            if (productoExistente.Precio_venta == 0)
                throw new AppValidationException("No se puede actualizar un precio venta nulo");


            //Validamos que haya al menos un cambio en las propiedades
            if (unProducto.Equals(productoExistente))
                throw new AppValidationException("No hay cambios en los atributos del producto. No se realiza actualización.");

            try
            {
                bool resultadoAccion = await _productRepository
                    .UpdateAsync(producto_id, unProducto);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                productoExistente = await _productRepository
                    .GetByIdAsync(unProducto.Id!);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return productoExistente;

        }
    }
}
