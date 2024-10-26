//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using TranSQL.shared.models;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace TranSQL.server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TiposInspeccionController : ControllerBase
//    {
//        private readonly TranSQLDbContext _context;

//        public TiposInspeccionController(TranSQLDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/<TiposInspeccionController>
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<TipoInspeccion>>> GetTiposInspeccion()
//        {
//            return await _context.TipoInspecciones.ToListAsync();
//        }

//        // GET api/<TiposInspeccionController>/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<TipoInspeccion>> GetTipoInspeccion(int id)
//        {
//            var tipoInspeccion = await _context.TipoInspecciones.FindAsync(id);
//            if (tipoInspeccion == null)
//            {
//                return NotFound();
//            }

//            //return Ok(tipoInspeccion);
//            return tipoInspeccion;
//        }

//        // POST api/<TiposInspeccionController>
//        [HttpPost]
//        public async Task<ActionResult<TipoInspeccion>> PostTipoInspeccion(TipoInspeccion tipoInspeccion)
//        {
//            _context.TipoInspecciones.Add(tipoInspeccion);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetTipoInspeccion", new { id = tipoInspeccion.IdTipoInspeccion }, tipoInspeccion);

//        }

//        // PUT api/<TiposInspeccionController>/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutTipoInspeccion(int id, TipoInspeccion tipoInspeccion)
//        {
//            if (id != tipoInspeccion.IdTipoInspeccion)
//            {
//                return BadRequest();
//            }
//            _context.Entry(tipoInspeccion).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//            return NoContent();
//        }

//        // DELETE api/<TiposInspeccionController>/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteTipoInspeccion(int id)
//        {
//            var tipoInspeccion = await _context.TipoInspecciones.FindAsync(id);
//            if (tipoInspeccion == null)
//            {
//                return NotFound();
//            }
//            _context.TipoInspecciones.Remove(tipoInspeccion);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}
