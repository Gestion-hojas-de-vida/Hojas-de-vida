using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GHV.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GHV.Controllers
{
    [Route("[controller]")]
    public class RecuperaContrasenaController : Controller
    {
        private readonly BaseContext _context;

        public RecuperaContrasenaController(BaseContext context)
        {
            _context = context;
        }

        public IActionResult RecuperaContrasena()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> RecuperaContrasena(string email)
        {
            email = "danitorresm734@gmail.com";
            if (string.IsNullOrEmpty(email))
            {
                ViewData["Mensaje"] = "Por favor ingrese un correo electrónico válido.";
                return View();
            }
          
            var usuario = await  _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
            {
                
                 
                ViewData["Mensaje"] = $"No se encontró ningún usuario registrado con el correo electrónico {email}.";
                return RedirectToAction("Home", "Privacy");
                
            }
            /*HttpContext.Session.SetString("Names", usuario.Nombre);*/
            // Aquí deberías implementar la lógica para enviar el correo de recuperación
            // Simulamos enviando un mensaje de éxito
            ViewData["Mensaje"] = $"Se ha enviado un correo de recuperación a {email}";

            return View("Home","Index");
        }
    }
}