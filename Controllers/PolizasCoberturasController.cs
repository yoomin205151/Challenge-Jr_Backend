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
    public class PolizasCoberturasController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PolizasCoberturasController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PolizasCoberturas>> GetAllPolizasCoberturas()
        {
            // Lógica para obtener todas las polizas desde la base de datos
            var polizascoberturas = _dbContext.Polizas;
            return Ok(polizascoberturas);
        }

        [HttpGet("{id}")]
        public ActionResult<PolizasCoberturas> GetPolizasCoberturasById(int id)
        {
            var polizascoberturas = _dbContext.Polizas.Find(id);
            if (polizascoberturas == null)
            {
                return NotFound();
            }
            return Ok(polizascoberturas);
        }

        [HttpPost]
        public ActionResult<PolizasCoberturas> CreatePolizasCoberturas(PolizasCoberturas polizascoberturas)
        {
            // Lógica para crear una nueva cobertura en la base de datos
            _dbContext.PolizasCoberturas.Add(polizascoberturas);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetPolizasCoberturasById), new { id = polizascoberturas.Id }, polizascoberturas);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePolizasCoberturas(int id, PolizasCoberturas polizascoberturas)
        {
            // Lógica para actualizar una poliza en la base de datos
            var existingPolizasCoberturas = _dbContext.PolizasCoberturas.Find(id);
            if (existingPolizasCoberturas == null)
            {
                return NotFound();
            }
            existingPolizasCoberturas.Cobertura = polizascoberturas.Cobertura;
            existingPolizasCoberturas.Poliza = polizascoberturas.Poliza;
            existingPolizasCoberturas.MontoAsegurado = polizascoberturas.MontoAsegurado;           
            // Actualiza otros campos de acuerdo a tu modelo de datos
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePolizasCoberturas(int id)
        {
            // Lógica para eliminar una poliza de la base de datos
            var polizascoberturas = _dbContext.PolizasCoberturas.Find(id);
            if (polizascoberturas == null)
            {
                return NotFound();
            }
            _dbContext.PolizasCoberturas.Remove(polizascoberturas);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
