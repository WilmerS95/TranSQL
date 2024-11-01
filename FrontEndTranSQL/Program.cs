using CurrieTechnologies.Razor.SweetAlert2;
using TranSQL.client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using TranSQL.client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuración de HttpClient para el servidor
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7223") });

// Servicio de autenticación JWT
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<TokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<TokenAuthenticationStateProvider>());

//builder.Services.AddScoped<VehiculoService>();
builder.Services.AddScoped<ReservacionService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
//builder.Services.AddScoped<IEmailService, EmailService>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://192.168.1.58:7223") });

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
