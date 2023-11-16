using Dapper;
using ERP_LicoExpress_API.DbContexts;
using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Helpers;
using Npgsql;
using System.Data;
using System.Numerics;

namespace ERP_LicoExpress_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PgsqlDbContext contextoDB;

        public UserRepository(PgsqlDbContext contexto)
        {
            contextoDB = contexto;

        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT id, correo, contrasena, rol, sede_id " +
                        "FROM usuarios u ";

            var resultadoProducts = await conexion.QueryAsync<User>(sentenciaSQL,
                                        new DynamicParameters());

            return resultadoProducts;
        }
        public async Task<bool> CreateAsync(User user)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "p_inserta_usuario";
                var parametros = new
                {
                    p_correo = user.Correo,
                    p_contrasena = user.Contrasena,
                    p_rol = user.Rol,
                    p_sede_id=user.Sede_id
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
        public async Task<bool> DeleteAsync(User user)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "p_eliminar_usuario";
                var parametros = new
                {
                    p_user_id = user.Id
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

        public async Task<User> GetByCorreoAsync(string correo)
        {
            User user = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@correo", correo,
                                    DbType.String, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, correo, contrasena , rol, sede_id " +
                "FROM usuarios " +
                "WHERE correo = @correo";

            var resultado = await conexion.QueryAsync<User>(sentenciaSQL,
                                parametrosSentencia);

            if (resultado.Any())
            {
                user = resultado.First();

            }

            return user;
        }

        public async Task<User> GetByIdAsync(int user_id)
        {
            User user = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@usuario_id", user_id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, correo, contrasena " +
                "FROM usuarios " +
                "WHERE id = @usuario_id";

            var resultado = await conexion.QueryAsync<User>(sentenciaSQL,
                                parametrosSentencia);

            if (resultado.Any())
            {
                user = resultado.First();

            }

            return user;
        }

        

    }
}
