
using GHV.Models;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;

namespace GHV.Controllers
{
    [Route("[controller]")]
    public class MailController : Controller
    {
        [HttpPost]
        public void SendConfirmationEmail(Usuario usuario)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("GHV Recordar Contraseña", "pruebariwi@gmail.com"));
            message.To.Add(new MailboxAddress("", usuario.Email));
            message.Subject = "Recover-Password";
            message.Body = new TextPart("plain")
            {
                Text = $"Hola {usuario.Nombre} tu contraseña es: {usuario.Contrasena}"
            };

            using (var client = new SmtpClient())
            {
                try
                {

                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("pruebariwi@gmail.com", "cijm vdzz fmza opwu"); // Usa la contraseña de la aplicación aquí
                    client.Send(message);
                }
                catch (SmtpCommandException ex)
                {
                    Console.WriteLine($"Error SMTP: {ex.StatusCode}");
                    throw;
                }
                catch (SmtpProtocolException ex)
                {
                    Console.WriteLine($"Error de protocolo SMTP: {ex.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al enviar el correo electrónico: {ex.Message}");
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

    }
}