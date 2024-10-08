using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TranSQL.Shared;
using TranSQL.Server.Data;
using Microsoft.EntityFrameworkCore;


namespace TranSQL.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Departamento>>> GetDepartamento()
        {
            var lista = await _context.Departamento.ToListAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> GetSingleDepartamento(int id)
        {
            var miobjeto = await _context.Departamento.FindAsync(id);
            if (miobjeto == null)
            {
                return NotFound("No se encontró el departamento.");
            }

            return Ok(miobjeto);
        }

        [HttpPost]
        public async Task<ActionResult<Departamento>> CreateDepartamento(Departamento objeto)
        {
            _context.Departamento.Add(objeto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSingleDepartamento), new { id = objeto.IdDepartamento }, objeto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartamento(int id, Departamento objeto)
        {
            if (id != objeto.IdDepartamento)
            {
                return BadRequest("Los IDs no coinciden.");
            }

            _context.Entry(objeto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Departamento.Any(e => e.IdDepartamento == id))
                {
                    return NotFound("No se encontró el departamento.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Departamento>> DeleteDepartamento(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
            {
                return NotFound("No se encontró el departamento.");
            }

            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();

            return Ok(departamento);
        }
    }
}