using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using GestionTransporte_CS_API_PostgresSQL.Helpers;

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
    }

}
