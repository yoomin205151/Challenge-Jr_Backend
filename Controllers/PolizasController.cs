using Microsoft.AspNetCore.Mvc;
using AdminPolizasAPI.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AdminPolizasAPI;

namespace AdminPolizasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolizasController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PolizasController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Poliza>> GetAllPolizas()
        {
            try
            {
                var polizas = _dbContext.Polizas;
                return Ok(polizas);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las pólizas.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Poliza> GetPolizaById(int id)
        {
            try
            {
                var poliza = _dbContext.Polizas.Find(id);
                if (poliza == null)
                {
                    return NotFound();
                }
                return Ok(poliza);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la póliza.");
            }
        }

        [HttpPost]
        public ActionResult<Poliza> CreatePoliza(Poliza poliza)
        {
            try
            {
                _dbContext.Polizas.Add(poliza);
                _dbContext.SaveChanges();
                return CreatedAtAction(nameof(GetPolizaById), new { id = poliza.Id }, poliza);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la póliza.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePoliza(int id, Poliza poliza)
        {
            try
            {
                var existingPoliza = _dbContext.Polizas.Find(id);
                if (existingPoliza == null)
                {
                    return NotFound();
                }
                existingPoliza.Nombre = poliza.Nombre;
                // Actualizar otros campos según tu modelo de datos
                _dbContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la póliza.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePoliza(int id)
        {
            try
            {
                var poliza = _dbContext.Polizas.Find(id);
                if (poliza == null)
                {
                    return NotFound();
                }
                _dbContext.Polizas.Remove(poliza);
                _dbContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la póliza.");
            }
        }
    }
}
