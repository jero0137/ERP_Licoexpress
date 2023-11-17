using ERP_LicoExpress_API.Helpers;
using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Repositories;

namespace ERP_LicoExpress_API.Services
{
    public class InventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILocationRepository _locationRepository;

        public InventoryService(IInventoryRepository inventoryRepository, ILocationRepository locationRepository)
        {
            _inventoryRepository = inventoryRepository;
            _locationRepository = locationRepository;
        }


        public async Task<IEnumerable<InventoryDetailed>> GetByLocationAsync(int location_id)
        {
            var losInventories= await _inventoryRepository
                .GetByLocationAsync(location_id);

            if (losInventories.Count() == 0)
                throw new AppValidationException($"Inventario no encontrado para la sede {location_id}");

            return losInventories;
        }

        public async Task<Inventory> GetByIdAsync(int inventory_id)
        {
            var unInventory = await _inventoryRepository
                .GetByIdAsync(inventory_id);

            if (unInventory.Id == 0)
                throw new AppValidationException($"Inventario no encontrado con el id {inventory_id}");

            return unInventory;
        }


        public async Task<IEnumerable<InventoryDetailed>> GetByLocationProductAsync(int location_id , int product_id)
        {
            var losInventories = await _inventoryRepository
                .GetByLocationProductAsync(location_id, product_id);

            if (losInventories.Count() == 0)
                throw new AppValidationException($"Inventario no encontrado para la sede {location_id} y el producto {product_id}");

            return losInventories;
        }



        public async Task<Inventory> CreateAsync(int location_id, Inventory unInventory)
        {
            if (location_id == 0)
                throw new AppValidationException("No se puede insertar un inventario sin sede");

            if (unInventory.Producto_id == 0)
                throw new AppValidationException("No se puede insertar un inventario sin un producto");

            if (unInventory.Fecha_registro.Length == 0)
                throw new AppValidationException("No se puede insertar un inventario sin un una fecha de vencimiento");

            if (unInventory.Lote == 0)
                throw new AppValidationException("No se puede insertar un inventario sin un lote");

            if (unInventory.Stock == 0)
                throw new AppValidationException("No se puede insertar un inventario sin un stock");


            try
            {
                bool resultadoAccion = await _inventoryRepository.CreateAsync(location_id, unInventory);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                return unInventory;

            }
            catch (DbOperationException error)
            {
                throw error;
            }


        }


        public async Task<Inventory> UpdateAsync(int inventory_id, int location_id, Inventory unInventory)
        {


            var inventoryExistente = await _inventoryRepository
                .GetByIdAsync(inventory_id);
            var locationExistente = await _locationRepository
                .GetByIdAsync(location_id);


            if (inventoryExistente.Id == 0)
                throw new AppValidationException($"No existe un autobus registrado con el id {unInventory.Id}");

            if (locationExistente.Id == 0)
                throw new AppValidationException($"No existe una sede registrado con el id {location_id}");

            if (inventory_id != inventoryExistente.Id)
                throw new AppValidationException($"Inconsistencia en el Id de la sede a actualizar. Verifica argumentos");


            if (string.IsNullOrEmpty(inventoryExistente.Fecha_registro))
                throw new AppValidationException("No se puede actualizar una fecha de vencimiento nula");

            if (inventoryExistente.Lote == 0)
                throw new AppValidationException("No se puede actualizar un lote nulo");

            if (inventoryExistente.Sede_id == 0)
                throw new AppValidationException("No se puede actualizar una sede nula");

            if (inventoryExistente.Producto_id == 0)
                throw new AppValidationException("No se puede actualizar un producto nulo");

            if (inventoryExistente.Stock == 0)
                throw new AppValidationException("No se puede actualizar un stock nulo");


            //Validamos que haya al menos un cambio en las propiedades
            if (unInventory.Equals(inventoryExistente))
                throw new AppValidationException("No hay cambios en los atributos de la sede. No se realiza actualización.");

            try
            {
                bool resultadoAccion = await _inventoryRepository
                    .UpdateAsync(inventory_id, location_id, unInventory);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                inventoryExistente = await _inventoryRepository
                    .GetByIdAsync(unInventory.Id!);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return inventoryExistente;

        }
    }
}
