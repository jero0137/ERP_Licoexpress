using System.Data;
using Dapper;
using ERP_LicoExpress_API.DbContexts;
using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Helpers;
using Npgsql;

namespace ERP_LicoExpress_API.Repositories
{
    public class LocationRepository:ILocationRepository
    {
        private readonly PgsqlDbContext contextoDB;
        public LocationRepository(PgsqlDbContext unContexto)
        {
            contextoDB = unContexto;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT id, nombre, direccion, nombre_admin, contacto_admin, telefono_admin, ciudad  " +
                        "FROM sedes " +
                        "ORDER BY id DESC";

            var resultadoLocations = await conexion.QueryAsync<Location>(sentenciaSQL,
                                        new DynamicParameters());

            return resultadoLocations;
        }


        public async Task<Location> GetByIdAsync(int id)
        {
            Location unaLocation = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@location_id", id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, nombre, direccion, nombre_admin, contacto_admin, telefono_admin, ciudad  " +
                        "FROM sedes " +
                        "WHERE id=@location_id";

            var resultado = await conexion.QueryAsync<Location>(sentenciaSQL,
                                parametrosSentencia);

            if (resultado.Any())
                unaLocation = resultado.First();

            return unaLocation;
        }

        public async Task<Location> GetByNameAsync(string name)
        {
            Location unaLocation = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@location_name", name,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, nombre, direccion, nombre_admin, contacto_admin, telefono_admin, ciudad  " +
                        "FROM sedes " +
                        "WHERE nombre=@location_name";

            var resultado = await conexion.QueryAsync<Location>(sentenciaSQL,
                                parametrosSentencia);

            if (resultado.Any())
                unaLocation = resultado.First();

            return unaLocation;
        }

        public async Task<bool> DeleteAsync(Location unaLocation)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "p_elimina_location";
                var parametros = new
                {
                    p_id = unaLocation.Id
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

        public async Task<bool> CreateAsync(Location unaLocation)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "p_inserta_location";
                var parametros = new
                {
                    p_nombre = unaLocation.Nombre,
                    p_direccion = unaLocation.Direccion,
                    p_nombre_admin = unaLocation.Nombre_admin,
                    p_contacto_admin = unaLocation.Contacto_admin,
                    p_telefono_admin = unaLocation.Telefono_admin,
                    p_ciudad = unaLocation.Ciudad,
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
