using Dapper;
using ERP_LicoExpress_API.DbContexts;
using ERP_LicoExpress_API.Helpers;
using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using Npgsql;
using System.Data;

namespace ERP_LicoExpress_API.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly PgsqlDbContext contextoDB;
        public InventoryRepository(PgsqlDbContext unContexto)
        {
            contextoDB = unContexto;
        }


        public async Task<IEnumerable<InventoryDetailed>> GetByLocationAsync(int id)
        {
            Inventory unInventory = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@location_id", id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT i.id, s.nombre sede, p.nombre producto, fecha_vencimiento, lote, stock  " +
                        "FROM inventarios i " +
                        "JOIN sedes s ON s.id=i.sede_id "+
                        "JOIN productos p ON p.id=i.producto_id " +
                        "WHERE sede_id=@location_id";

            var resultadoInventories = await conexion.QueryAsync<InventoryDetailed>(sentenciaSQL,
                                parametrosSentencia);

            return resultadoInventories;
        }

        

    }
}
