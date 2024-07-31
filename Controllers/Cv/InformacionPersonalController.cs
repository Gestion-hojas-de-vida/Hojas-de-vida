using Microsoft.AspNetCore.Mvc;
using GHV.Models;
using GHV.Data;
using System.Threading.Tasks;

namespace GHV.Controllers
{
    public class InformacionPersonalController : Controller
    {
        private readonly BaseContext _context;

        public InformacionPersonalController(BaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GuardarInformacionPersonal(InformacionPersonal infoPersonal)
        {
            if (ModelState.IsValid)
            {
                _context.InformacionesPersonales.Add(infoPersonal);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Usuario", "User"); // Asegúrate de que esto redirige a la vista correcta con el modelo
        }

        // Añadir una acción para mostrar el formulario
        [HttpGet]
        public IActionResult CrearInformacionPersonal()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MostrarInformacionPersonal(int id)
        {
            // Buscar la información personal con el ID proporcionado
            var infoPersonal = await _context.InformacionesPersonales.FindAsync(id);
            
            if (infoPersonal == null)
            {
                return NotFound(); // O puedes redirigir a una vista de error
            }

            return View(infoPersonal); // Pasar el modelo a la vista
        }
    }
}
