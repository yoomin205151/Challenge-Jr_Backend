using AdminPolizasAPI.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AdminPolizasAPI;

namespace AdminPolizasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoberturasController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CoberturasController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cobertura>> GetAllCoberturas()
        {
            try
            {
                // Lógica para obtener todas las coberturas desde la base de datos
                var coberturas = _dbContext.Coberturas;
                return Ok(coberturas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Cobertura> GetCoberturaById(int id)
        {
            try
            {
                var cobertura = _dbContext.Coberturas.Find(id);
                if (cobertura == null)
                {
                    return NotFound();
                }
                return Ok(cobertura);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Cobertura> CreateCobertura(Cobertura cobertura)
        {
            try
            {
                // Lógica para crear una nueva cobertura en la base de datos
                _dbContext.Coberturas.Add(cobertura);
                _dbContext.SaveChanges();
                return CreatedAtAction(nameof(GetCoberturaById), new { id = cobertura.Id }, cobertura);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCobertura(int id, Cobertura cobertura)
        {
            try
            {
                // Lógica para actualizar una cobertura en la base de datos
                var existingCobertura = _dbContext.Coberturas.Find(id);
                if (existingCobertura == null)
                {
                    return NotFound();
                }
                existingCobertura.Nombre = cobertura.Nombre;
                // Actualiza otros campos de acuerdo a tu modelo de datos
                _dbContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCobertura(int id)
        {
            try
            {
                // Lógica para eliminar una cobertura de la base de datos
                var cobertura = _dbContext.Coberturas.Find(id);
                if (cobertura == null)
                {
                    return NotFound();
                }
                _dbContext.Coberturas.Remove(cobertura);
                _dbContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
