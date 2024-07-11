using Microsoft.AspNetCore.Mvc;

namespace GHV.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
