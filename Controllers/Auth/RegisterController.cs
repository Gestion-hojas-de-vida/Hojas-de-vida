using Microsoft.AspNetCore.Mvc;
using GHV.Models;
using GHV.Data;
using Microsoft.AspNetCore.Authentication;


namespace GHV.Controllers
{
    public class RegisterController : Controller
    {
        public readonly BaseContext _context;

        public RegisterController (BaseContext context)
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
                    SendRegisterController EmailREgister = new SendRegisterController();
                    EmailREgister.SendEmailConfirmation(usuario);
                    
                    _context.Usuarios.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "Login");
                }
                catch (Exception)
                {
                    // Manejar errores aquí, por ejemplo, loggear el error y mostrar un mensaje de error al usuario
                    ModelState.AddModelError("", "Error al intentar registrar el usuario. Por favor, intenta nuevamente.");
                    return View(); 
                }
            }
            

            return View(usuario); // Retornar la vista con el modelo de usuario si el modelo no es válido
        }
        
    }
}
