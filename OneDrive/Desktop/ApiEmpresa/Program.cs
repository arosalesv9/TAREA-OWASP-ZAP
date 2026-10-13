using Microsoft.EntityFrameworkCore;
using ApiEmpresa.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<Conexiones>(opt =>
    opt.UseMySQL(builder.Configuration.GetConnectionString("ConexionMySQL")??"otracadena"));

var app = builder.Build();

// 1. COLOCAR EL MIDDLEWARE AQUÍ ARRIBA (Antes de Swagger, MapControllers y HttpsRedirection)
app.Use(async (context, next) =>
{
    // Protege contra ataques de Clickjacking
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    // Previene el sniffing de MIME types
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    // Protección XSS
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    // Control de Referrer
    context.Response.Headers.Append("Referrer-Policy", "no-referrer");
    // Configuración básica de Content Security Policy (CSP)
    context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; script-src 'self' 'unsafe-inline' 'unsafe-eval'; style-src 'self' 'unsafe-inline'; img-src 'self' data:;");

    await next();
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();