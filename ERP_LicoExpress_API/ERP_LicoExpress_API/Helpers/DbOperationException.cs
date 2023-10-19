namespace GestionTransporte_CS_API_PostgresSQL.Helpers
{
    public class DbOperationException : Exception
    {
        public DbOperationException(string message) : base(message) { }
    }
}
