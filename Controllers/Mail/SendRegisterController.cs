using System.Text;
using System.Text.Json;
using GHV.Models;
using Microsoft.AspNetCore.Mvc;

namespace GHV.Controllers
{
    [Route("[controller]")]
    public class SendRegisterController : Controller
    {
        [HttpPost]
        public async Task SendEmailConfirmation(Usuario usuario)
        {
            try
            {
                // URL de destino para la solicitud POST
                string url = "https://api.mailersend.com/v1/email";

                // Token de autorización para la solicitud POST
                string jwtToken = "mlsn.0e4c27612fe5cba702364c859da4dea97241e1683d1a7cf518e1ef7effc7fa24";

                // Crear el mensaje de correo electrónico utilizando datos del usuario
                var emailMessage = new
                {
                    from = new { email = "PeakyBlinders@trial-vywj2lpyn0ml7oqz.mlsender.net" },
                    to = new[]
                    {
                        new { email = usuario.Email}
                    },
                    subject = "Correo Confirmacion",
                    text = $"Hola {usuario.Nombre}, su registro fue exitoso.",
                    
                };

                // Serializar el objeto EmailMessage en formato JSON
                string jsonBody = JsonSerializer.Serialize(emailMessage);

                // Crear un objeto HttpClient para realizar la solicitud HTTP
                using (HttpClient client = new HttpClient())
                {
                    // Configurar el encabezado Content-Type para indicar que el cuerpo es JSON
                    client.DefaultRequestHeaders.Add("ContentType", "Application/Json");
                    // Configurar el encabezado Authorization para indicar el token de autorización
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwtToken}");

                    // Crear el contenido de la solicitud POST como StringContent
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Realizar la solicitud POST a la URL especificada
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Verificar si la solicitud fue exitosa (código de estado 200-299)
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Correo electrónico enviado correctamente:");
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        // Si la solicitud no fue exitosa, mostrar el código de estado
                        Console.WriteLine($"La solicitud falló con el código de estado: {response.StatusCode}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al enviar correo electrónico: {e.Message}");
            }
        }

    }
}