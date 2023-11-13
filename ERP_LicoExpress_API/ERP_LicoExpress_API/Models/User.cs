using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ERP_LicoExpress_API.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = string.Empty;

        [JsonPropertyName("contrasena")]
        public string Contrasena { get; set; } = string.Empty;

        [JsonPropertyName("rol")]
        public string Rol { get; set; } = string.Empty;

        [JsonPropertyName("sede_id")]
        public int Sede_id { get; set; } = 0;


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
