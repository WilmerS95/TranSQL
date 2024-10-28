using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared.DTO;
using TranSQL.shared.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradoresController : ControllerBase
    {
        private readonly TranSQLDbContext _context;

        public ColaboradoresController(TranSQLDbContext context)
        {
            _context = context;
        }

        // GET: api/<ColaboradoresController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colaborador>>> GetColaboradores()
        {
            return await _context.Colaboradores
                .Include(c => c.Departamento)
                .ToListAsync();
        }

        // GET api/<ColaboradoresController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colaborador>> GetColaborador(int id)
        {
            var colaborador = await _context.Colaboradores
               .Include(c => c.Departamento)
               .FirstOrDefaultAsync(c => c.IdColaborador == id);

            if (colaborador == null)
            {
                return NotFound();
            }

            return colaborador;
        }

        // POST api/<ColaboradoresController>
        [HttpPost]
        public async Task<ActionResult<Colaborador>> PostColaborador([FromBody] ColaboradorCreateDTO colaboradorDto)
        {
            // Verificar que el IdDepartamento está presente
            if (colaboradorDto.IdDepartamento == 0)
            {
                return BadRequest(new { message = "El IdDepartamento es obligatorio." });
            }

            // Buscar el departamento existente en la base de datos
            var departamentoExistente = await _context.Departamentos.FindAsync(colaboradorDto.IdDepartamento);

            if (departamentoExistente == null)
            {
                return BadRequest(new { message = "El Departamento especificado no existe." });
            }

            // Crear el nuevo objeto Colaborador usando el DTO y asignar el departamento
            var nuevoColaborador = new Colaborador
            {
                PrimerNombre = colaboradorDto.PrimerNombre,
                SegundoNombre = colaboradorDto.SegundoNombre,
                PrimerApellido = colaboradorDto.PrimerApellido,
                SegundoApellido = colaboradorDto.SegundoApellido,
                ApellidoDeCasada = colaboradorDto.ApellidoDeCasada,
                Correo = colaboradorDto.Correo,
                Password = colaboradorDto.Password,
                IdDepartamento = colaboradorDto.IdDepartamento,
                Departamento = departamentoExistente
            };

            // Agregar el nuevo colaborador
            _context.Colaboradores.Add(nuevoColaborador);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el colaborador: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetColaborador), new { id = nuevoColaborador.IdColaborador }, nuevoColaborador);
        }

        // PUT api/<ColaboradoresController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaborador(int id, Colaborador colaborador)
        {
            if (id != colaborador.IdColaborador)
            {
                return BadRequest();
            }

            _context.Entry(colaborador).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<ColaboradoresController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            var colaborador = await _context.Colaboradores.FindAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            _context.Colaboradores.Remove(colaborador);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}