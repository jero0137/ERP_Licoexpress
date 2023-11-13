using System.Text.Json.Serialization;

namespace ERP_LicoExpress_API.Models
{
    public class Product
    {


        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("tipo_id")]
        public int Tipo_id { get; set; } = 0;


        [JsonPropertyName("tamaño")]
        public string Tamaño { get; set; } = string.Empty;

        [JsonPropertyName("imagen")]
        public string Imagen { get; set; } = string.Empty;

        [JsonPropertyName("precio_base")]
        public float Precio_base { get; set; } = 0F;

        [JsonPropertyName("precio_venta")]
        public float Precio_venta { get; set; } = 0F;

        [JsonPropertyName("proveedor_id")]
        public int Proveedor_id { get; set; } = 0;


        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otroProduct = (Product)obj;
            return Id == otroProduct.Id
            && Nombre.Equals(otroProduct.Nombre)
            && Tipo_id == otroProduct.Tipo_id
            && Tamaño.Equals(otroProduct.Tamaño)
            && Imagen == otroProduct.Imagen
            && Precio_base == otroProduct.Precio_base
            && Precio_venta == otroProduct.Precio_venta
            && Proveedor_id == otroProduct.Proveedor_id;

        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + Id.GetHashCode();
                hash = hash * 5 + (Nombre?.GetHashCode() ?? 0);
                hash = hash * 5 + Tipo_id.GetHashCode();
                hash = hash * 5 + (Tamaño?.GetHashCode() ?? 0);
                hash = hash * 5 + (Imagen?.GetHashCode() ?? 0);
                hash = hash * 5 + Precio_base.GetHashCode();
                hash = hash * 5 + Precio_venta.GetHashCode();
                hash = hash * 5 + Proveedor_id.GetHashCode();
                return hash;
            }
        }
    }
}
