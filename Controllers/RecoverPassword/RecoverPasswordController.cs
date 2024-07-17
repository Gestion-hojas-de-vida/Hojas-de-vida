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
        //base de datos
        private readonly BaseContext _context;

        //constructor de mi controlador 

        public RecoverPasswordController(BaseContext context)
        {
            _context = context;
        }
        //retorna la vista 
        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(string email)
        {
            /*se condiciona si el email mandado como parametro si es nulo o esta vacio*/
            if (string.IsNullOrEmpty(email))
            {
                //si esta luno o vacio no fuen encontrado da este mensaje
                ViewData["Mensaje"] = "Por favor ingrese un correo electrónico válido.";
            }
            //igualamos el valor de input email y loigualamos al parametro
            ViewBag.Email =  email;
            //se consulta si correo del usuario existe
            var usuario = await  _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            //si no existe 
            if (usuario == null)
            {
               
                ViewData["Mensaje"] = $"No se encontró ningún usuario registrado con el correo electrónico {email}.";
               
            }
 
            //creamos una instaciaci del controlador email
            MailController Email = new MailController();
            //ejecutamos el metodo del controlador con el usuario como parametro
            Email.SendConfirmationEmail(usuario!);

            //si todo esta bien a este punto significa que el correo fue encontrado
            return RedirectToAction("Login", "Login");

        }
    }
}