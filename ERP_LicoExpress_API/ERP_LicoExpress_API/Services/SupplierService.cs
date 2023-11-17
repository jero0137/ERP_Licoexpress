using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Helpers;
using ERP_LicoExpress_API.Repositories;

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


        public async Task<Supplier> GetByIdAsync(int supplier_id)
        {
            var unSupplier = await _supplierRepository
                .GetByIdAsync(supplier_id);

            if (unSupplier.Id == 0)
                throw new AppValidationException($"Proveedor no encontrado con el id {supplier_id}");

            return unSupplier;
        }


        public async Task DeleteAsync(int supplier_id)
        {
            // validamos que el supplier a eliminar si exista con ese Id
            var supplierExistente = await _supplierRepository. GetByIdAsync(supplier_id);

            if (supplierExistente.Id == 0)
                throw new AppValidationException($"No existe un proveedor con el Id {supplier_id} que se pueda eliminar");

            try
            {
                bool resultadoAccion = await _supplierRepository
                    .DeleteAsync(supplierExistente);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");
            }
            catch (DbOperationException error)
            {
                throw error;
            }
        }


        public async Task<Supplier> CreateAsync(Supplier unSupplier)
        {
            if (unSupplier.Nombre_empresa.Length == 0)
                throw new AppValidationException("No se puede insertar un proveedor sin un nombre");

            if (unSupplier.Responsable.Length == 0)
                throw new AppValidationException("No se puede insertar un proveedor sin un responsable");

            if (unSupplier.Correo.Length == 0)
                throw new AppValidationException("No se puede insertar un proveedor sin un correo");

            if (unSupplier.Numero_registro == 0)
                throw new AppValidationException("No se puede insertar un proveedor sin un número de registro");

            if (unSupplier.Numero_contacto.Length == 0)
                throw new AppValidationException("No se puede insertar un proveedor sin un número de contacto");

            if (unSupplier.Direccion_empresa.Length == 0)
                throw new AppValidationException("No se puede insertar un proveedor sin una dirección");

            if (unSupplier.Ciudad.Length == 0)
                throw new AppValidationException("No se puede insertar un proveedor sin una ciudad");


            var supplierExistente = await _supplierRepository
                .GetByIdAsync(unSupplier.Id!);

            try
            {
                bool resultadoAccion = await _supplierRepository.CreateAsync(unSupplier);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                supplierExistente = await _supplierRepository.GetByIdAsync(unSupplier.Id);
                //throw new AppValidationException("Inserción correcta");

            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return (supplierExistente);

        }


        public async Task<Supplier> UpdateAsync(int supplier_id, Supplier unProveedor)
        {


            var supplierExistente = await _supplierRepository
                .GetByIdAsync(supplier_id);

            if (supplierExistente.Id == 0)
                throw new AppValidationException($"No existe un proveedor registrado con el id {unProveedor.Id}");

            if (supplier_id != supplierExistente.Id)
                throw new AppValidationException($"Inconsistencia en el Id de la sede a actualizar. Verifica argumentos");


            if (supplierExistente.Numero_registro == 0)
                throw new AppValidationException("No se puede actualizar un numero de registro nulo");

            if (string.IsNullOrEmpty(supplierExistente.Numero_contacto))
                throw new AppValidationException("No se puede actualizar un numero de contacto nulo");

            if (string.IsNullOrEmpty(supplierExistente.Correo))
                throw new AppValidationException("No se puede actualizar un correo nulo");

            if (string.IsNullOrEmpty(supplierExistente.Nombre_empresa))
                throw new AppValidationException("No se puede actualizar un nombre nulo");

            if (string.IsNullOrEmpty(supplierExistente.Ciudad))
                throw new AppValidationException("No se puede actualizar una ciudad nula");

            if (string.IsNullOrEmpty(supplierExistente.Direccion_empresa))
                throw new AppValidationException("No se puede actualizar una direccion nula");

            if (string.IsNullOrEmpty(supplierExistente.Responsable))
                throw new AppValidationException("No se puede actualizar un responsable nulo");

            //Validamos que haya al menos un cambio en las propiedades
            if (unProveedor.Equals(supplierExistente))
                throw new AppValidationException("No hay cambios en los atributos del producto. No se realiza actualización.");

            try
            {
                bool resultadoAccion = await _supplierRepository
                    .UpdateAsync(supplier_id, unProveedor);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                supplierExistente = await _supplierRepository
                    .GetByIdAsync(unProveedor.Id!);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return supplierExistente;

        }
    }

}
