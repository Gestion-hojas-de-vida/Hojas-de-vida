using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHV.Data;
using Microsoft.AspNetCore.Mvc;

namespace GHV.Admin
{
    public class AdminController : Controller
    {
         private readonly BaseContext _context;
        public AdminController(BaseContext context)
        {
            _context = context;
        }
        /*administrador*/
        public IActionResult Administrador()
        {

            return View();  
        }
    }
}