using System.Text.Json.Serialization;

namespace ERP_LicoExpress_API.Models
{
    public class UserDetailed
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = string.Empty;

        [JsonPropertyName("rol")]
        public string Rol { get; set; } = string.Empty;

        [JsonPropertyName("sede")]
        public string Sede { get; set; } = string.Empty;
    }
}
