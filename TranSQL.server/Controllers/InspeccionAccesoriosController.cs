using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspeccionAccesoriosController : ControllerBase
    {
        private readonly TranSQLDbContext _context;

        public InspeccionAccesoriosController(TranSQLDbContext context)
        {
            _context = context;
        }

        // GET: api/<InspeccionAccesoriosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InspeccionAccesorio>>> GetInspeccionAccesorios()
        {
            return await _context.InspeccionesAccesorios.ToListAsync();
        }

        // GET api/<InspeccionAccesoriosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InspeccionAccesorio>> GetInspeccionAccesorio(int id)
        {
            var inspeccionAccesorio = await _context.InspeccionesAccesorios.FindAsync(id);
            if (inspeccionAccesorio == null)
            {
                return NotFound();
            }

            //return Ok(inspeccionAccesorio);
            return inspeccionAccesorio;
        }

        // POST api/<InspeccionAccesoriosController>
        [HttpPost]
        public async Task<ActionResult<InspeccionAccesorio>> PostInspeccionAccesorio(InspeccionAccesorio inspeccionAccesorio)
        {
            _context.InspeccionesAccesorios.Add(inspeccionAccesorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInspeccionAccesorio", new { id = inspeccionAccesorio.IdInspeccion }, inspeccionAccesorio);

        }

        // PUT api/<InspeccionAccesoriosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInspeccionAccesorio(int id, InspeccionAccesorio inspeccionAccesorio)
        {
            if (id != inspeccionAccesorio.IdInspeccion)
            {
                return BadRequest();
            }
            _context.Entry(inspeccionAccesorio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<InspeccionAccesoriosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspeccionAccesorio(int id)
        {
            var inspeccionAccesorio = await _context.InspeccionesAccesorios.FindAsync(id);
            if (inspeccionAccesorio == null)
            {
                return NotFound();
            }
            _context.InspeccionesAccesorios.Remove(inspeccionAccesorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
