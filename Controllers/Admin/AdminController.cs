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