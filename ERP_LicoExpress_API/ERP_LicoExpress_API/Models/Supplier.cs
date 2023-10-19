using System.Numerics;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ERP_LicoExpress_API.Models
{
    public class Supplier
    {

        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("numero_contacto")]
        public string Numero_contacto { get; set; } = string.Empty;

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otroSupplier = (Supplier)obj;
            return Id == otroSupplier.Id
            && Nombre.Equals(otroSupplier.Nombre)
            && Numero_contacto.Equals(otroSupplier.Numero_contacto)
            && Correo.Equals(otroSupplier.Correo);

        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + Id.GetHashCode();
                hash = hash * 5 + (Nombre?.GetHashCode() ?? 0);
                hash = hash * 5 + (Numero_contacto?.GetHashCode() ?? 0);
                hash = hash * 5 + (Correo?.GetHashCode() ?? 0);

                return hash;
            }
        }


    }
}
