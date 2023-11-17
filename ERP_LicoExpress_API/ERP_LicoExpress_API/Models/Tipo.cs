using System.Text.Json.Serialization;

namespace ERP_LicoExpress_API.Models
{
    public class Tipo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
    }
}
