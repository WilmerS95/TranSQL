using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoriosController : ControllerBase
    {
        private readonly TranSQLDbContext _context;

        public AccesoriosController(TranSQLDbContext context)
        {
            _context = context;
        }

        // GET: api/<AccesoriosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accesorio>>> GetAccesorios()
        {
            return await _context.Accesorios.ToListAsync();
        }

        // GET api/<AccesoriosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accesorio>> GetAccesorio(int id)
        {
            var accesorio = await _context.Accesorios.FindAsync(id);
            if (accesorio == null)
            {
                return NotFound();
            }

            //return Ok(accesorio);
            return accesorio;
        }

        // POST api/<AccesoriosController>
        [HttpPost]
        public async Task<ActionResult<Accesorio>> PostAccesorio(Accesorio accesorio) {
            _context.Accesorios.Add(accesorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccesorio", new {id = accesorio.IdAccesorio}, accesorio);
        
        }

        // PUT api/<AccesoriosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccesorio(int id, Accesorio accesorio)
        {
            if (id != accesorio.IdAccesorio)
            {
                return BadRequest();
            }
            _context.Entry(accesorio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<AccesoriosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccesorio(int id)
        {
            var accesorio = await _context.Accesorios.FindAsync(id);
            if (accesorio == null)
            {
                return NotFound();
            }
            _context.Accesorios.Remove(accesorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
