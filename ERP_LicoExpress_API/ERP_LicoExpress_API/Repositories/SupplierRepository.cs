using Dapper;
using ERP_LicoExpress_API.DbContexts;
using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using Npgsql;

namespace ERP_LicoExpress_API.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly PgsqlDbContext contextoDB;


        public SupplierRepository(PgsqlDbContext unContexto)
        {
            contextoDB = unContexto;
        }


        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT id, nombre, numero_contacto, correo " +
                        "FROM proveedores " +
                        "ORDER BY id DESC";

            var resultadoSuppliers = await conexion.QueryAsync<Supplier>(sentenciaSQL,
                                        new DynamicParameters());

            return resultadoSuppliers;
        }

    }
}
