using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using GHV.Data;
using GHV.Models;

namespace GHV.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly string Key;
        private readonly string Issuer;
        private readonly string Audience;
        private readonly IConfiguration _configuration;
        private readonly BaseContext _context;

        public LoginController(IConfiguration configuration,BaseContext context)
        {
            _context = context;
            Key = configuration["Jwt:Key"];
            Issuer = configuration["Jwt:Issuer"];
            Audience = configuration["Jwt:Audience"];
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
            Console.WriteLine("Empieza");
            var LoginUser = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Contrasena == contrasena);

            if (LoginUser != null)
            {
                // Obtener los roles asociados al usuario
                    var roles = _context.RolesDeModelos
                        .Where(mr => mr.ModeloId == LoginUser.Id)  // Filtrar por el Id del modelo (ej. usuario)
                        .Join(_context.Roles,
                            mr => mr.RolId,
                            r => r.Id,
                            (mr, r) => r)
                        .ToList();


                /* aqui validamos si es nombre de l rol es admin o user */
                if (roles.Any(r => r.Nombre == "Admin"))
                {
                    
                    var token = GenerateJwtToken(LoginUser.Email);
                    Console.WriteLine("token" + token);
                    ViewBag.Nombre=LoginUser.Nombre;
                    ViewBag.SuccessMessages = "Inicio de sesión exitoso" + LoginUser.Nombre;
                    return RedirectToAction("Administrador", "Admin");
                }
                else if (roles.Any(r => r.Nombre == "User"))
                {
                    var token = GenerateJwtToken(LoginUser.Email);
                    
                    Console.WriteLine("token" + token);
                    ViewBag.Nombre=LoginUser.Nombre;
                    ViewBag.SuccessMessages = "Inicio de sesión exitoso" + LoginUser.Nombre;
                    return View("Logeado", "Login");
                }
                else
                {
                    ViewBag.ErrorMessages = "El usuario no tiene un rol asignado.";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessages = "Credenciales incorrectas";
                return View();
            }
            /*--------------------------------------------------------------------------------------------------------------*/

            
        }

        /*generacion de token*/
         public string GenerateJwtToken(string username)
            {
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
            }
    }
}