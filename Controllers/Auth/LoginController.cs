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
            var LoginUser = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Contrasena == contrasena);

            if (LoginUser != null)
            {
                // Obtener los roles asociados al usuario
                    var roles = _context.RolesDeModelos
                        .Where(mr => mr.ModeloId == LoginUser.Id)  // Filtrar por el Id del modelo (ej. usuario)
                        .Join(_context.Roles,
                            mr => mr.RolId,
                            r => r.Id,
                            (mr, r) => r)
                        .ToList();


                /* aqui validamos si es nombre de l rol es admin o user */
                if (roles.Any(r => r.Nombre == "Admin"))
                {
                    return RedirectToAction("Administrador", "Admin");
                }
                else if (roles.Any(r => r.Nombre == "User"))
                {
                      
                ViewBag.Nombre=LoginUser.Nombre;
                ViewBag.SuccessMessages = "Inicio de sesión exitoso";
                return View("Logeado", "Login");
                }
                else
                {
                    ViewBag.ErrorMessages = "El usuario no tiene un rol asignado.";
                    return View();
                }
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