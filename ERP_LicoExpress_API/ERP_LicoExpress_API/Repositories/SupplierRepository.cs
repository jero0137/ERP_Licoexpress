using System.Data;
using Dapper;
using ERP_LicoExpress_API.DbContexts;
using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using GestionTransporte_CS_API_PostgresSQL.Helpers;
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

            string sentenciaSQL = "SELECT id, nombre_empresa, responsable, correo, numero_registro, numero_contacto, direccion_empresa, ciudad  " +
                        "FROM proveedores " +
                        "ORDER BY id DESC";

            var resultadoSuppliers = await conexion.QueryAsync<Supplier>(sentenciaSQL,
                                        new DynamicParameters());

            return resultadoSuppliers;
        }


        public async Task<Supplier> GetByIdAsync(int id)
        {
            Supplier unSupplier = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@supplier_id", id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, nombre_empresa, responsable, correo, numero_registro, numero_contacto, direccion_empresa, ciudad  " +
                        "FROM proveedores " +
                        "WHERE id=@supplier_id";

            var resultado = await conexion.QueryAsync<Supplier>(sentenciaSQL,
                                parametrosSentencia);

            if (resultado.Any())
                unSupplier = resultado.First();

            return unSupplier;
        }


        public async Task<bool> DeleteAsync(Supplier unSupplier)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "p_elimina_supplier";
                var parametros = new
                {
                    p_id = unSupplier.Id
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


        public async Task<bool> CreateAsync(Supplier unSupplier)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "p_inserta_supplier";
                var parametros = new
                {
                    p_nombre_empresa = unSupplier.Nombre_empresa,
                    p_responsable = unSupplier.Responsable,
                    p_correo = unSupplier.Correo,
                    p_numero_registro = unSupplier.Numero_registro,
                    p_numero_contacto = unSupplier.Numero_contacto,
                    p_direccion_empresa = unSupplier.Direccion_empresa,
                    p_ciudad = unSupplier.Ciudad,
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
