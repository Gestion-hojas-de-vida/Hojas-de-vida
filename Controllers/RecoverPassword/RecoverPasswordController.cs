using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GHV.Data;
using GHV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GHV.Controllers
{
    [Route("[controller]")]
    public class RecoverPasswordController : Controller
    {
        private readonly BaseContext _context;

        public RecoverPasswordController(BaseContext context)
        {
            _context = context;
        }

        public IActionResult RecoverPassword()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> RecoverPassword(string email)
        {

            
            email = "danitorresm734@gmail.com";
            if (string.IsNullOrEmpty(email))
            {
                ViewData["Mensaje"] = "Por favor ingrese un correo electrónico válido.";
            }
          
            var usuario = await  _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);


            if (usuario == null)
            {
                
                ViewData["Mensaje"] = $"No se encontró ningún usuario registrado con el correo electrónico {email}.";

            }
            /*HttpContext.Session.SetString("Names", usuario.Nombre);*/
            // Aquí deberías implementar la lógica para enviar el correo de recuperación
            // Simulamos enviando un mensaje de éxito
            ViewData["Mensaje"] = $"Se ha enviado un correo de recuperación a {email}";

            MailController Email = new MailController();
            Email.EnviarCorreo(usuario);

            return View("Index");


        }
    }
}