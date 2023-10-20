using Dapper;
using ERP_LicoExpress_API.DbContexts;
using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using GestionTransporte_CS_API_PostgresSQL.Helpers;
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
        public async Task<bool> CreateAsync(User user)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "core.p_inserta_user";
                var parametros = new
                {
                    p_correo = user.Correo,
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

        public async Task<User> GetByCorreo(string correo)
        {
            User user = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@correo", correo,
                                    DbType.String, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, correo, contrasena " +
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

        public async Task<User> GetByID(int user_id)
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

        //Funcion de repositorio para crear una sesion una vez se haga login
        public async Task<bool> CreateSessionAsync(Session sesion)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "core.p_inserta_sesion";
                var parametros = new
                {
                    p_token = sesion.Token
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
