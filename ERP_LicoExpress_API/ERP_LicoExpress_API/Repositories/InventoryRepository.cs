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



        public async Task<Inventory> GetByIdAsync(int id)
        {
            Inventory unInventory = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@inventory_id", id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, sede_id, producto_id, fecha_registro, lote, stock  " +
                        "FROM inventarios " +
                        "WHERE id=@inventory_id";

            var resultado = await conexion.QueryAsync<Inventory>(sentenciaSQL,
                                parametrosSentencia);

            if (resultado.Any())
                unInventory = resultado.First();

            return unInventory;
        }

        public async Task<IEnumerable<InventoryDetailed>> GetByLocationAsync(int id)
        {
            Inventory unInventory = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@location_id", id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT i.id, s.nombre sede, p.nombre producto, fecha_registro, lote, stock  " +
                        "FROM inventarios i " +
                        "JOIN sedes s ON s.id=i.sede_id "+
                        "JOIN productos p ON p.id=i.producto_id " +
                        "WHERE sede_id=@location_id";

            var resultadoInventories = await conexion.QueryAsync<InventoryDetailed>(sentenciaSQL,
                                parametrosSentencia);

            return resultadoInventories;
        }

        public async Task<IEnumerable<InventoryDetailed>> GetByLocationProductAsync(int location_id, int product_id)
        {
            Inventory unInventory = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@location_id", location_id,
                                    DbType.Int32, ParameterDirection.Input);

            parametrosSentencia.Add("@product_id", product_id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT i.id, s.nombre sede, p.nombre producto, fecha_registro, lote, stock  " +
                        "FROM inventarios i " +
                        "JOIN sedes s ON s.id=i.sede_id " +
                        "JOIN productos p ON p.id=i.producto_id " +
                        "WHERE sede_id=@location_id and producto_id=@product_id";

            var resultadoInventories = await conexion.QueryAsync<InventoryDetailed>(sentenciaSQL,
                                parametrosSentencia);

            return resultadoInventories;
        }

        public async Task<bool> CreateAsync(int location_id, Inventory unInventory)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "p_inserta_inventory";
                var parametros = new
                {
                    p_sede_id = location_id,
                    p_producto_id = unInventory.Producto_id,
                    p_fecha_registro = unInventory.Fecha_registro,
                    p_lote = unInventory.Lote,
                    p_stock = unInventory.Stock
                };

                var cantidad_filas = await conexion.ExecuteAsync(
                    procedimiento,
                    parametros,
                    commandType: CommandType.StoredProcedure);

                if (cantidad_filas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }

        public async Task<IEnumerable<InventoryDetailed>> GetByProductId(int producto_id)
        {
            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@product_id", producto_id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT i.id, s.nombre sede, p.nombre producto, fecha_registro, lote, stock  " +
                        "FROM inventarios i " +
                        "JOIN sedes s ON s.id=i.sede_id " +
                        "JOIN productos p ON p.id=i.producto_id " +
                        "WHERE i.producto_id=@product_id";

            var resultadoInventories = await conexion.QueryAsync<InventoryDetailed>(sentenciaSQL,
                                parametrosSentencia);

            return resultadoInventories;
        }

        public async Task<CantProductLocation> GetCantProductByLocationId(int location_id)
        {
            var conexion = contextoDB.CreateConnection();

            CantProductLocation cant = new CantProductLocation();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@location_id", location_id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, sede, producto, cantidad " +
                        "FROM cantidad_productosxsede cp " +
                        "WHERE id=@location_id";

            var resultado = await conexion.QueryAsync<CantProductLocation>(sentenciaSQL,
                                parametrosSentencia);

            if (resultado.Any())
                cant = resultado.First();

            return cant;
        }

        public async Task<bool> UpdateAsync(int inventory_id, int location_id, Inventory unInventory)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "p_actualiza_inventory";
                var parametros = new
                {
                    p_sede_id = location_id,
                    p_id = inventory_id,
                    p_producto_id = unInventory.Producto_id,
                    p_fecha_registro = unInventory.Fecha_registro,
                    p_lote = unInventory.Lote,
                    p_stock = unInventory.Stock

                };

                var cantidad_filas = await conexion.ExecuteAsync(
                    procedimiento,
                    parametros,
                    commandType: CommandType.StoredProcedure);

                if (cantidad_filas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }
    }
}
