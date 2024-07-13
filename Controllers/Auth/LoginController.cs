using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GHV.Data;
using GHV.Models;

namespace GHV.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly BaseContext _context;
        public LoginController(BaseContext context)
        {
            _context = context;
        }
        /*Direccionamiento a la vita de Login*/
        [HttpGet]
        public IActionResult Login()
        {
            return View();  
        }
        /*------------------------------------------------*/
        /*Verificacion de credenciales para Login*/
        [HttpPost]
        public IActionResult Login(string email, string contrasena)
        {   
            Console.WriteLine("Empieza");
            var LoginUser =_context.Usuarios.FirstOrDefault(u => u.Email == email && u.Contrasena == contrasena);
            if (LoginUser != null)
            {   
                ViewBag.Nombre=LoginUser.Nombre;
                ViewBag.SuccessMessages = "Inicio de sesión exitoso";
                return View("Logeado", "Login");

            }
            else
            {   
                ViewBag.ErrorMessages = "Credenciales incorrectas";
                return View();
            }
        }
        /*--------------------------------------------------------------------------------------------------------------*/
    }
}