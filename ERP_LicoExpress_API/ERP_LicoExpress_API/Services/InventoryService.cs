using ERP_LicoExpress_API.Helpers;
using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Repositories;

namespace ERP_LicoExpress_API.Services
{
    public class InventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<InventoryDetailed>> GetByLocationAsync(int location_id)
        {
            var losInventories= await _inventoryRepository
                .GetByLocationAsync(location_id);

            if (losInventories.Count() == 0)
                throw new AppValidationException($"Inventario no encontrado para la sede {location_id}");

            return losInventories;
        }


        public async Task<IEnumerable<InventoryDetailed>> GetByLocationProductAsync(int location_id , int product_id)
        {
            var losInventories = await _inventoryRepository
                .GetByLocationProductAsync(location_id, product_id);

            if (losInventories.Count() == 0)
                throw new AppValidationException($"Inventario no encontrado para la sede {location_id} y el producto {product_id}");

            return losInventories;
        }



        public async Task<Inventory> CreateAsync(Inventory unInventory)
        {
            if (unInventory.Sede_id == 0)
                throw new AppValidationException("No se puede insertar un inventario sin sede");

            if (unInventory.Producto_id == 0)
                throw new AppValidationException("No se puede insertar un inventario sin un producto");

            if (unInventory.Fecha_vencimiento.Length == 0)
                throw new AppValidationException("No se puede insertar un inventario sin un una fecha de vencimiento");

            if (unInventory.Lote == 0)
                throw new AppValidationException("No se puede insertar un inventario sin un lote");

            if (unInventory.Stock == 0)
                throw new AppValidationException("No se puede insertar un inventario sin un stock");


            try
            {
                bool resultadoAccion = await _inventoryRepository.CreateAsync(unInventory);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                return unInventory;

            }
            catch (DbOperationException error)
            {
                throw error;
            }


        }



    }
}
