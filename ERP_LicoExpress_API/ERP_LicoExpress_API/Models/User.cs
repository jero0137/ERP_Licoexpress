using System.Text.RegularExpressions;

namespace ERP_LicoExpress_API.Models
{
    public class User
    {
        public int Id { get; set; } = 0;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + Id.GetHashCode();
                hash = hash * 5 + (Correo?.GetHashCode() ?? 0);

                if (hash < 0) hash *= -1;

                return hash;
            }
        }
    }
}
