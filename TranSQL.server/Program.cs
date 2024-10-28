using TranSQL.server;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TranSQL.server.Models;
using TranSQL.server.Services;
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

// Configuración JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthorization(options =>
{
    // Política para permitir acceso solo a Logística
    options.AddPolicy("LogisticaOnly", policy =>
    {
        policy.RequireClaim("Departamento", "Logistica");
    });
});

// Agregar autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
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
                     .AllowCredentials();
    });
});

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<EmailService>();
builder.Services.AddScoped<IEmailService, EmailService>();

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

// Habilitar autenticación JWT
app.UseAuthentication();

// Habilitar autorización (JWT + roles)
app.UseAuthorization();

//// Autorización (si tienes middleware de autenticación, este iría antes de la autorización)
//app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Iniciar la aplicación
app.Run();
