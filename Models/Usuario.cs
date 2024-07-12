using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHV.Models
{
    public class Usuario
    {
        public int Id {get; set;}
        public string? Nombre {get; set;}
        public string? Apellido {get; set;}
        public string? Email {get; set;}
        public string? Contrasena {get; set;}
        public string? GoogleId {get; set;}
        public DateOnly FechaRegistro {get; set;}
    }
}

