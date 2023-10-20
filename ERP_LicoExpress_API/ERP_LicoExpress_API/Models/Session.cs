namespace ERP_LicoExpress_API.Models
{
    public class Session
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Fecha_creacion { get; set; } = DateTime.Now;
        public DateTime Fecha_terminacion {  get; set; } = DateTime.Now;
    }
}
