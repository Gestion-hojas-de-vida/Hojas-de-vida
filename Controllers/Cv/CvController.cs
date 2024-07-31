using Microsoft.AspNetCore.Mvc;

namespace GHV.Controllers
{
    public class CvController : Controller
    {
         public IActionResult InformacionAcademica()
        {
            return View();
        }
        public IActionResult InformacionLaboral()
        {
            return View();
        }
        public IActionResult Habilidades()
        {
            return View();
        }
        public IActionResult InformacionPersonal()
        {
            return View();
        }
    }
}