using System.Text.Json.Serialization;

namespace ERP_LicoExpress_API.Models
{
    public class CantProductLocation
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("sede")]
        public string Sede { get; set; } = string.Empty;

        [JsonPropertyName("producto")]
        public string Producto { get; set; } = string.Empty;

        [JsonPropertyName("proveedor")]
        public string Proveedor { get; set; } = string.Empty;

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; } = string.Empty;

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; } = 0;

    }
}
