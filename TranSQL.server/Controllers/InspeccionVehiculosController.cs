﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranSQL.shared.models;
using TranSQL.shared.DTO;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspeccionVehiculosController : ControllerBase
    {
        private readonly TranSQLDbContext _context;
        private readonly string _imagenesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes_inspeccion");

        public InspeccionVehiculosController(TranSQLDbContext context)
        {
            _context = context;

            if (!Directory.Exists(_imagenesPath))
            {
                Directory.CreateDirectory(_imagenesPath);
            }
        }

        // Método de mapeo para convertir InspeccionVehiculo a InspeccionVehiculoDTO
        private InspeccionVehiculoDTO MapToDTO(InspeccionVehiculo inspeccion)
        {
            return new InspeccionVehiculoDTO
            {
                IdInspeccion = inspeccion.IdInspeccion,
                FechaInspeccion = inspeccion.FechaInspeccion,
                Observaciones = inspeccion.Observaciones ?? string.Empty,
                OdometroInicial = inspeccion.OdometroInicial,
                OdometroFinal = inspeccion.OdometroFinal,
                ImagenRuta = inspeccion.ImagenRuta ?? string.Empty,
                IdReservacion = inspeccion.IdReservacion,
                ColaboradorNombre = inspeccion.Colaborador != null ? $"{inspeccion.Colaborador.PrimerNombre} {inspeccion.Colaborador.PrimerApellido}" : "Sin Asignar",
                AccesorioNombre = inspeccion.Accesorio?.NombreAccesorio ?? "Sin Accesorio",
                TipoInspeccionNombre = inspeccion.TipoInspeccion?.NombreTipoInsepccion ?? "Tipo No Especificado"
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InspeccionVehiculoDTO>> ObtenerInspeccion(int id)
        {
            var inspeccion = await _context.InspeccionVehiculos
                .Include(i => i.Colaborador)
                .Include(i => i.Accesorio)
                .Include(i => i.TipoInspeccion)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.IdInspeccion == id);

            if (inspeccion == null)
                return NotFound();

            var inspeccionDTO = MapToDTO(inspeccion);
            return Ok(inspeccionDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InspeccionVehiculoDTO>>> GetInspeccionVehiculos()
        {
            try
            {
                var inspecciones = await _context.InspeccionVehiculos
                    .Include(i => i.Colaborador)
                    .Include(i => i.Accesorio)
                    .Include(i => i.TipoInspeccion)
                    .AsNoTracking()
                    .ToListAsync();

                var inspeccionesDTO = inspecciones.Select(inspeccion => new InspeccionVehiculoDTO
                {
                    IdInspeccion = inspeccion.IdInspeccion,
                    FechaInspeccion = inspeccion.FechaInspeccion,
                    Observaciones = inspeccion.Observaciones ?? string.Empty,
                    OdometroInicial = inspeccion.OdometroInicial ?? 0,
                    OdometroFinal = inspeccion.OdometroFinal ?? 0,
                    ImagenRuta = inspeccion.ImagenRuta ?? string.Empty,
                    IdReservacion = inspeccion.IdReservacion,
                    ColaboradorNombre = inspeccion.Colaborador != null
                        ? $"{inspeccion.Colaborador.PrimerNombre ?? "Sin nombre"} {inspeccion.Colaborador.PrimerApellido ?? "Sin apellido"}"
                        : "Sin Asignar",
                    AccesorioNombre = inspeccion.Accesorio?.NombreAccesorio ?? "Sin Accesorio",
                    TipoInspeccionNombre = inspeccion.TipoInspeccion?.NombreTipoInsepccion ?? "Tipo No Especificado"
                }).ToList();

                return Ok(inspeccionesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener inspecciones: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<ActionResult<InspeccionVehiculoDTO>> PostInspeccionVehiculo(InspeccionVehiculo inspeccionVehiculo)
        {
            _context.InspeccionVehiculos.Add(inspeccionVehiculo);
            await _context.SaveChangesAsync();

            var inspeccionDTO = MapToDTO(inspeccionVehiculo);
            return CreatedAtAction(nameof(ObtenerInspeccion), new { id = inspeccionVehiculo.IdInspeccion }, inspeccionDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInspeccionVehiculo(int id, InspeccionVehiculo inspeccionVehiculo)
        {
            if (id != inspeccionVehiculo.IdInspeccion)
            {
                return BadRequest();
            }

            _context.Entry(inspeccionVehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspeccionVehiculo(int id)
        {
            var inspeccionVehiculo = await _context.InspeccionVehiculos.FindAsync(id);
            if (inspeccionVehiculo == null)
            {
                return NotFound();
            }

            _context.InspeccionVehiculos.Remove(inspeccionVehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/imagen")]
        public async Task<IActionResult> SubirImagen(int id, IFormFile imagen)
        {
            var inspeccion = await _context.InspeccionVehiculos.FindAsync(id);
            if (inspeccion == null)
                return NotFound("Inspección no encontrada.");

            if (imagen == null || imagen.Length == 0)
                return BadRequest("Imagen no válida.");

            var imagenNombre = $"{id}_{Path.GetRandomFileName()}{Path.GetExtension(imagen.FileName)}";
            var rutaCompleta = Path.Combine(_imagenesPath, imagenNombre);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }

            inspeccion.ImagenRuta = Path.Combine("imagenes_inspeccion", imagenNombre);
            _context.Entry(inspeccion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { RutaImagen = inspeccion.ImagenRuta });
        }
    }
}
