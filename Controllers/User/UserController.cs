using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GHV.Controllers.User
{
    public class UserController : Controller
    {

        public IActionResult Usuario()
        {
            return View();
        }

    
    }
}