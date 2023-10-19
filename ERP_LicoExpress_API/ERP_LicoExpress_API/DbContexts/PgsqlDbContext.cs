using System.Data;
using Npgsql;

namespace ERP_LicoExpress_API.DbContexts
{
    public class PgsqlDbContext
    {
        private readonly string cadenaConexion;
        public PgsqlDbContext(IConfiguration unaConfiguracion)
        {
            cadenaConexion = unaConfiguracion.GetConnectionString("PgSql")!;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(cadenaConexion);
        }
    }
}
