using Microsoft.AspNetCore.Mvc;
using AdminPolizasAPI.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            // Lógica para obtener todas las polizas desde la base de datos
            var polizas = _dbContext.Polizas;
            return Ok(polizas);
        }

        [HttpGet("{id}")]
        public ActionResult<Poliza> GetPolizaById(int id)
        {
            var poliza = _dbContext.Polizas.Find(id);
            if (poliza == null)
            {
                return NotFound();
            }
            return Ok(poliza);
        }

        [HttpPost]
        public ActionResult<Poliza> CreatePoliza(Poliza poliza)
        {
            // Lógica para crear una nueva cobertura en la base de datos
            _dbContext.Polizas.Add(poliza);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetPolizaById), new { id = poliza.Id }, poliza);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePoliza(int id, Poliza poliza)
        {
            // Lógica para actualizar una poliza en la base de datos
            var existingPolizas = _dbContext.Polizas.Find(id);
            if (existingPolizas == null)
            {
                return NotFound();
            }
            existingPolizas.Nombre = poliza.Nombre;
            // Actualiza otros campos de acuerdo a tu modelo de datos
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePoliza(int id)
        {
            // Lógica para eliminar una poliza de la base de datos
            var poliza = _dbContext.Polizas.Find(id);
            if (poliza == null)
            {
                return NotFound();
            }
            _dbContext.Polizas.Remove(poliza);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
