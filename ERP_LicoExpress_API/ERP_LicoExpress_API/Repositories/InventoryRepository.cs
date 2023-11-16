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


        public async Task<Inventory> GetByLocationAsync(int id)
        {
            Inventory unInventory = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@location_id", id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, sede_id, producto_id, fecha_vencimiento, lote, stock  " +
                        "FROM inventarios " +
                        "WHERE sede_id=@location_id";

            var resultado = await conexion.QueryAsync<Inventory>(sentenciaSQL,
                                parametrosSentencia);

            if (resultado.Any())
                unInventory = resultado.First();

            return unInventory;
        }

        

    }
}
