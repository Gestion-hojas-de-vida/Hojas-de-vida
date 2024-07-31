namespace GHV.Models
{
    public class InformacionPersonal
    {
        public int Id { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? EstadoCivil { get; set; }
        public string? PaisNacimiento { get; set; }
        public string? DepartamentoNacimiento { get; set; }
    }
}
