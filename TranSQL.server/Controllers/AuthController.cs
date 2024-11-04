using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TranSQL.shared.models;
using TranSQL.shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TranSQLDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(TranSQLDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO login)
        {
            // Validar el usuario
            var user = await _context.Colaboradores
                .Include(c => c.Departamento)
                .FirstOrDefaultAsync(c => c.Correo == login.Username && c.Password == login.Password);

            if (user == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            // Mapear el usuario al DTO
            var userDto = new ColaboradorDTO
            {
                IdColaborador = user.IdColaborador,
                PrimerNombre = user.PrimerNombre,
                PrimerApellido = user.PrimerApellido,
                Correo = user.Correo,
                Departamento = new DepartamentoDTO
                {
                    IdDepartamento = user.Departamento.IdDepartamento,
                    NombreDepartamento = user.Departamento.NombreDepartamento
                }
            };

            // Generar el token JWT
            var token = GenerateJwtToken(userDto);
            return Ok(new { Token = token, Usuario = userDto });
        }

        private string GenerateJwtToken(ColaboradorDTO user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Correo),
                new Claim("Departamento", user.Departamento.NombreDepartamento),
                new Claim("IdColaborador", user.IdColaborador.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
