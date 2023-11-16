using System.Text.Json.Serialization;

namespace ERP_LicoExpress_API.Models
{
    public class Inventory
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("sede_id")]
        public int Sede_id { get; set; } = 0;

        [JsonPropertyName("producto_id")]
        public int Producto_id { get; set; } = 0;

        [JsonPropertyName("fecha_vencimiento")]
        public string Fecha_vencimiento { get; set; } = String.Empty;

        [JsonPropertyName("lote")]
        public int Lote { get; set; } = 0;

        [JsonPropertyName("stock")]
        public int Stock { get; set; } = 0;


        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otroInventario = (Inventory)obj;
            return Id == otroInventario.Id
            && Sede_id == otroInventario.Sede_id
            && Producto_id == otroInventario.Producto_id
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
                hash = hash * 5 + Sede_id.GetHashCode();
                hash = hash * 5 + Producto_id.GetHashCode();
                hash = hash * 5 + (Fecha_vencimiento?.GetHashCode() ?? 0);
                hash = hash * 5 + Lote.GetHashCode();
                hash = hash * 5 + Stock.GetHashCode();
                return hash;
            }
        }
    }
}
