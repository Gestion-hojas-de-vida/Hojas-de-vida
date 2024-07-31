using DotNetEnv;
using GHV.Data;
using GHV.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cargar las variables de entorno desde el archivo .env
Env.Load();

// Obtener los valores de las variables de entorno
var googleClientId = "720930356197-qq25kkmiiciro2iuhsffg2892u251lr9.apps.googleusercontent.com";
var googleClientSecret = "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";

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BaseContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("MySqlConnection"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = googleClientId!;
    options.ClientSecret = googleClientSecret!;
});

var app = builder.Build();

// Verificar y agregar el rol "User" si no existe
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BaseContext>();
    SeedRoles(context);
}

void SeedRoles(BaseContext context)
{
    if (!context.Roles.Any(r => r.Nombre == "User"))
    {
        context.Roles.Add(new Rol
        {
            Nombre = "User",
            NombreGuardian = "default",
            CreadoEn = DateTime.Now,
            ActualizadoEn = DateTime.Now,
            Descripcion = "Rol de usuario por defecto"
        });
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
