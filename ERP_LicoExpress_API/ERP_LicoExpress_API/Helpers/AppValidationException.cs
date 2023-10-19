namespace GestionTransporte_CS_API_PostgresSQL.Helpers
{
    public class AppValidationException : Exception
    {
        public AppValidationException(string message) : base(message) { }

    }
}
