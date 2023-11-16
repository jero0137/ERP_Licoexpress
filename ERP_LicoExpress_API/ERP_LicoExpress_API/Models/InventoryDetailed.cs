using System.Text.Json.Serialization;

namespace ERP_LicoExpress_API.Models
{
    public class InventoryDetailed
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("sede")]
        public string Sede { get; set; } = string.Empty;

        [JsonPropertyName("producto")]
        public string Producto { get; set; } = string.Empty;

        [JsonPropertyName("fecha_vencimiento")]
        public string Fecha_vencimiento { get; set; } = string.Empty;

        [JsonPropertyName("lote")]
        public int Lote { get; set; } = 0;

        [JsonPropertyName("stock")]
        public int Stock { get; set; } = 0;


        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otroInventario = (InventoryDetailed)obj;
            return Id == otroInventario.Id
            && Sede.Equals(otroInventario.Sede)
            && Producto.Equals(otroInventario.Producto)
            && Fecha_vencimiento.Equals(otroInventario.Fecha_vencimiento)
            && Lote == otroInventario.Lote
            && Stock == otroInventario.Stock;

        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + Id.GetHashCode();
                hash = hash * 5 + (Sede?.GetHashCode() ?? 0);
                hash = hash * 5 + (Producto?.GetHashCode() ?? 0);
                hash = hash * 5 + (Fecha_vencimiento?.GetHashCode() ?? 0);
                hash = hash * 5 + Lote.GetHashCode();
                hash = hash * 5 + Stock.GetHashCode();
                return hash;
            }
        }
    }
}
