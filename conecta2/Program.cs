using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

// Configurar la autenticación con cookies.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Ruta de inicio de sesión.
    });

var app = builder.Build();

// Configurar el middleware de autenticación y autorización.
app.UseAuthentication(); // Habilita la autenticación
app.UseAuthorization();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // Ruta por defecto que redirige a Login
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}");

    // Ruta para Dispositivos
    endpoints.MapControllerRoute(
        name: "dispositivos",
        pattern: "Dispositivos/{action=Dispositivos}/{id?}",
        defaults: new { controller = "Dispositivos" });
});
app.Run();
