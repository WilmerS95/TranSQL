using TranSQL.server;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de la conexión a la base de datos.
builder.Services.AddDbContext<TranSQLDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

// Configuración de CORS
var corsPolicyName = "DefaultCorsPolicy";
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policyBuilder =>
    {
        // Permitir solo los orígenes especificados en la configuración
        policyBuilder.WithOrigins(allowedOrigins)
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials(); // Requiere que los orígenes sean específicos
    });
});

var app = builder.Build();

// Aplicar CORS antes de otros middlewares
app.UseCors(corsPolicyName);

// Configurar el pipeline de la solicitud HTTP.
if (app.Environment.IsDevelopment())
{
    // Configuración de Swagger solo en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirección HTTPS
app.UseHttpsRedirection();

// Autorización (si tienes middleware de autenticación, este iría antes de la autorización)
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Iniciar la aplicación
app.Run();
