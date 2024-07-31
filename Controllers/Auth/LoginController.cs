using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GHV.Data;
using GHV.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

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
            var LoginUser = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Contrasena == contrasena);

            if (LoginUser != null)
            {
                // Obtener los roles asociados al usuario
                var roles = _context.RolesDeModelos
                    .Where(mr => mr.ModeloId == LoginUser.Id)
                    .Join(_context.Roles,
                        mr => mr.RolId,
                        r => r.Id,
                        (mr, r) => r)
                    .Select(r => r.Nombre)
                    .ToList();

                ViewBag.NombreUsuario = LoginUser.Nombre;

                // Validar el rol del usuario
                if (roles.Contains("Admin"))
                {
                    ViewBag.Nombre = LoginUser.Nombre;
                    return RedirectToAction("Administrador", "Admin");
                }
                else
                {
                    // Redirigir a una página predeterminada si no hay rol asignado
                    ViewBag.Nombre = LoginUser.Nombre;
                    ViewBag.SuccessMessages = "Inicio de sesión exitoso, bienvenido " + LoginUser.Nombre;
                    return RedirectToAction("Usuario", "User");
                }
            }
            else
            {
                ViewBag.ErrorMessages = "Credenciales incorrectas";
                return View();
            }
        }

        /*--------------------------------------------------------------------------------------------------------------*/
        /*Autenticacion con google*/
        [HttpGet("GoogleLogin")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse")};
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result.Principal == null)
            {
                // Manejar la autenticación fallida
                return RedirectToAction("Login");
            }

            var claims = result.Principal.Identities
                .FirstOrDefault()?.Claims.Select(claim => new 
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                }).ToList();

            if (claims == null || !claims.Any())
            {
                //manejar la falta de claims
                return RedirectToAction("Login");
            }

            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (email != null)
            {
                var googleId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var nombre = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                // Verificar si GoogleId, Nombre y Apellido están presentes
                if (string.IsNullOrEmpty(googleId) || string.IsNullOrEmpty(nombre))
                {
                    // Manejar el caso donde GoogleId o Nombre son null o vacíos
                    return RedirectToAction("Login");
                }

                var user = _context.Usuarios.FirstOrDefault(u => u.Email == email);
                if (user == null)
                {
                    user = new Usuario
                    {
                        Email = email,
                        Nombre = nombre,
                        Apellido = "Apellido por defecto", 
                        GoogleId = googleId,
                        Contrasena = "Contraseña generada automáticamente" 
                    };

                    // Verificar si GoogleId está presente antes de agregar el usuario
                    if (string.IsNullOrEmpty(user.GoogleId))
                    {
                        // Manejar el caso donde GoogleId es null o vacío
                        return RedirectToAction("Login");
                    }

                    // Agregar el usuario a la base de datos si no está presente
                    _context.Usuarios.Add(user);
                    await _context.SaveChangesAsync();
                }

                var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                // Verificar si GoogleId está presente antes de agregar la reclamación
                if (!string.IsNullOrEmpty(user.GoogleId))
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.GoogleId));
                }
                
                claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.GoogleId!));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email!));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Nombre!));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Guardar el nombre del usuario en ViewBag
                ViewBag.NombreUsuario = user.Nombre; // Guardar el nombre del usuario en ViewBag
                
            }
            Console.WriteLine(ViewBag.NombreUsuario);
            return RedirectToAction("Usuario", "User");
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}