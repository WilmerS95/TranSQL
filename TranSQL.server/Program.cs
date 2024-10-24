using TranSQL.server;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de la conexi�n a la base de datos.
builder.Services.AddDbContext<TranSQLDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

// Configuraci�n de CORS
var corsPolicyName = "DefaultCorsPolicy";
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policyBuilder =>
    {
        // Permitir solo los or�genes especificados en la configuraci�n
        policyBuilder.WithOrigins(allowedOrigins)
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials(); // Requiere que los or�genes sean espec�ficos
    });
});

var app = builder.Build();

// Aplicar CORS antes de otros middlewares
app.UseCors(corsPolicyName);

// Configurar el pipeline de la solicitud HTTP.
if (app.Environment.IsDevelopment())
{
    // Configuraci�n de Swagger solo en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirecci�n HTTPS
app.UseHttpsRedirection();

// Autorizaci�n (si tienes middleware de autenticaci�n, este ir�a antes de la autorizaci�n)
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Iniciar la aplicaci�n
app.Run();
