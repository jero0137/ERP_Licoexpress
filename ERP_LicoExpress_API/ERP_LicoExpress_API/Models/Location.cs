using System.Text.Json.Serialization;

namespace ERP_LicoExpress_API.Models
{
    public class Location
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("direccion")]
        public string Direccion { get; set; } = string.Empty;


        [JsonPropertyName("nombre_admin")]
        public string Nombre_admin { get; set; } = string.Empty;

        [JsonPropertyName("contacto_admin")]
        public string Contacto_admin { get; set; } = string.Empty;


        [JsonPropertyName("telefono_admin")]
        public string Telefono_admin { get; set; } = string.Empty;

        [JsonPropertyName("ciudad")]
        public string Ciudad { get; set; } = string.Empty;



        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otraLocation = (Location)obj;
            return Id == otraLocation.Id
            && Nombre.Equals(otraLocation.Nombre)
            && Direccion.Equals(otraLocation.Direccion)
            && Nombre_admin.Equals(otraLocation.Nombre_admin)
            && Contacto_admin == otraLocation.Contacto_admin
            && Telefono_admin.Equals(otraLocation.Telefono_admin)
            && Ciudad.Equals(otraLocation.Ciudad);

        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + Id.GetHashCode();
                hash = hash * 5 + Nombre.GetHashCode();
                hash = hash * 5 + (Direccion?.GetHashCode() ?? 0);
                hash = hash * 5 + (Nombre_admin?.GetHashCode() ?? 0);
                hash = hash * 5 + (Contacto_admin?.GetHashCode() ?? 0);
                hash = hash * 5 + (Telefono_admin?.GetHashCode() ?? 0);
                hash = hash * 5 + (Ciudad?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
