using System.Numerics;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ERP_LicoExpress_API.Models
{
    public class Supplier
    {


        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("nombre_empresa")]
        public string Nombre_empresa { get; set; } = string.Empty;

        [JsonPropertyName("responsable")]
        public string Responsable { get; set; } = string.Empty;


        [JsonPropertyName("correo")]
        public string Correo { get; set; } = string.Empty;

        [JsonPropertyName("numero_registro")]
        public int Numero_registro { get; set; } = 0;


        [JsonPropertyName("numero_contacto")]
        public string Numero_contacto { get; set; } = string.Empty;

        [JsonPropertyName("direccion_empresa")]
        public string Direccion_empresa { get; set; } = string.Empty;

        [JsonPropertyName("ciudad")]
        public string Ciudad { get; set; } = string.Empty;


        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otroSupplier = (Supplier)obj;
            return Id == otroSupplier.Id
            && Nombre_empresa.Equals(otroSupplier.Nombre_empresa)
            && Responsable.Equals(otroSupplier.Responsable)
            && Correo.Equals(otroSupplier.Correo)
            && Numero_registro == otroSupplier.Numero_registro
            && Numero_contacto.Equals(otroSupplier.Numero_contacto)
            && Direccion_empresa.Equals(otroSupplier.Direccion_empresa)
            && Ciudad.Equals(otroSupplier.Ciudad);

        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 5 + Id.GetHashCode();
                hash = hash * 5 + Numero_registro.GetHashCode();
                hash = hash * 5 + (Nombre_empresa?.GetHashCode() ?? 0);
                hash = hash * 5 + (Numero_contacto?.GetHashCode() ?? 0);
                hash = hash * 5 + (Correo?.GetHashCode() ?? 0);
                hash = hash * 5 + (Responsable?.GetHashCode() ?? 0);
                hash = hash * 5 + (Ciudad?.GetHashCode() ?? 0);
                hash = hash * 5 + (Direccion_empresa?.GetHashCode() ?? 0);
                return hash;
            }
        }


    }
}
