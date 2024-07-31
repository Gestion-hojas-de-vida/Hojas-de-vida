using Microsoft.AspNetCore.Mvc;
using GHV.Models;
using GHV.Data;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace GHV.Controllers
{
    public class RegisterController : Controller
    {
        private readonly BaseContext _context;

        public RegisterController(BaseContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Enviar correo de confirmación
                    SendRegisterController EmailRegister = new SendRegisterController();
                    EmailRegister.SendRegisterEmail(usuario);

                    // Agregar el usuario a la base de datos
                    _context.Usuarios.Add(usuario);
                    await _context.SaveChangesAsync();

                    // Asignar el usuario al RolDeModelo
                    var rolDeModelo = new RolDeModelo
                    {
                        RolId = 2, // Asignar el RolId que corresponda
                        TipoDeModulo = "api/user/list", // Asignar el TipoDeModulo que corresponda
                        ModeloId = usuario.Id // Asumimos que 'ModeloId' se refiere al 'Id' del usuario
                    };

                    // Agregar la entidad sin seguimiento
                    _context.Entry(rolDeModelo).State = EntityState.Added;
                    await _context.SaveChangesAsync();

                    // Redirigir a la página de login después del registro exitoso
                    return RedirectToAction("Login", "Login");
                }
                catch (Exception ex)
                {
                    // Manejar errores aquí
                    ModelState.AddModelError("", "Error al intentar registrar el usuario. Por favor, intenta nuevamente. " + ex.Message);
                    return View(usuario); // Retornar la vista con el modelo de usuario si ocurre un error
                }
            }

            return View(usuario); // Retornar la vista con el modelo de usuario si el modelo no es válido
        }
    }
}
