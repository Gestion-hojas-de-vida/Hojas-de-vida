namespace GHV.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Contrasena { get; set; }
        public string? GoogleId { get; set; }
        public DateTime? FechaRegistro { get; set; } = DateTime.Now;
    }
}