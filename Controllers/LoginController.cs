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
        [HttpGet]
        public IActionResult Login()
        {
            return View();  
        }
    }
}