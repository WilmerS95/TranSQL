using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionesController : ControllerBase
    {
        private readonly TranSQLDbContext _context;

        public AsignacionesController(TranSQLDbContext context)
        {
            _context = context;
        }

        // POST: api/Asignacion
        [HttpPost]
        public async Task<IActionResult> AsignarVehiculos(int idSolicitud, List<string> placas)
        {
            // Validar si la solicitud existe
            var solicitud = await _context.SolicitudesReservacion.FindAsync(idSolicitud);
            if (solicitud == null)
                return NotFound("La solicitud no existe.");

            foreach (var placa in placas)
            {
                var vehiculo = await _context.Vehiculos.FindAsync(placa);
                if (vehiculo == null || vehiculo.IdEstadoVehiculo != 1) // 1 = Disponible
                    return BadRequest($"El vehículo {placa} no está disponible.");

                // Cambiar estado del vehículo
                vehiculo.IdEstadoVehiculo = 2; // 2 = Reservado

                // Crear la asignación
                var asignacion = new Asignacion
                {
                    Placa = placa,
                    IdSolicitud = idSolicitud,
                    IdColaborador = solicitud.IdColaborador ?? 0,
                    IdEstadoSolicitud = solicitud.IdEstadoSolicitud ?? 0
                };

                _context.Asignaciones.Add(asignacion);
                _context.Vehiculos.Update(vehiculo);
            }

            await _context.SaveChangesAsync();
            return Ok("Vehículos asignados correctamente.");
        }

        //// GET: api/<AsignacionesController>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Asignacion>>> GetAsignaciones()
        //{
        //    return await _context.Asignaciones.ToListAsync();
        //}

        //// GET api/<AsignacionesController>/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Asignacion>> GetAsignacion(int id)
        //{
        //    var asignacion = await _context.Asignaciones.FindAsync(id);
        //    if (asignacion == null)
        //    {
        //        return NotFound();
        //    }

        //    //return Ok(asignacion);
        //    return asignacion;
        //}

        //// POST api/<AsignacionesController>
        //[HttpPost]
        //public async Task<ActionResult<Asignacion>> PostAsignacion(Asignacion asignacion)
        //{
        //    _context.Asignaciones.Add(asignacion);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAsignacion", new { id = asignacion.idAsignacion }, asignacion);

        //}

        //// PUT api/<AsignacionesController>/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAsignacion(int id, Asignacion asignacion)
        //{
        //    if (id != asignacion.idAsignacion)
        //    {
        //        return BadRequest();
        //    }
        //    _context.Entry(asignacion).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}

        //// DELETE api/<AsignacionesController>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsignacion(int id)
        //{
        //    var asignacion = await _context.Asignaciones.FindAsync(id);
        //    if (asignacion == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Asignaciones.Remove(asignacion);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
