using AdminPolizasAPI.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AdminPolizasAPI;
using Microsoft.Data.SqlClient;

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

        [HttpGet("GetAllCoberturas")]
        public async Task<IActionResult> GetAllCoberturas()
        {
            try
            {

                var cobertura = await _dbContext.Coberturas.FromSqlRaw("EXEC SP_LISTCOBERTURAS")
                                .AsNoTracking()
                                .ToListAsync();

                if (cobertura.Count > 1)
                {
                    return Ok(cobertura);
                }
                else
                {
                    return NotFound("No existen registros de coberturas");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetCoberturaById/{id}")]
        public IActionResult GetCoberturaById(int id)
        {
            try
            {
                var cobertura = _dbContext.Coberturas.Find(id);
                if (cobertura == null)
                {
                    return NotFound($"No se encontró una cobertura con el ID: {id}");
                }
                return Ok(cobertura);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("CreateCobertura")]
        public async Task<IActionResult> CreateCobertura(Cobertura cobertura)
        {
            try
            {

                if (ModelState.IsValid && cobertura.Nombre != null)
                {

                    var nombre = cobertura.Nombre;
                    var responsabilidadCivil = cobertura.ResponsabilidadCivil ?? false;
                    var destruccionTotalAccidentes = cobertura.DestruccionTotalAccidentes ?? false;
                    var cristalesLaterales = cobertura.CristalesLaterales ?? false;
                    var lunetasParabrisas = cobertura.LunetasParabrisas ?? false;
                    var cerraduras = cobertura.Cerraduras ?? false;

                    var resultado = await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_CREATECOBERTURAS @nombre, @responsabilidadCivil, @destruccionTotalAccidentes, @cristalesLaterales, @lunetasParabrisas, @cerraduras",
                                            new SqlParameter("@nombre", nombre),
                                            new SqlParameter("@responsabilidadCivil", responsabilidadCivil),
                                            new SqlParameter("@destruccionTotalAccidentes", destruccionTotalAccidentes),
                                            new SqlParameter("@cristalesLaterales", cristalesLaterales),
                                            new SqlParameter("@lunetasParabrisas", lunetasParabrisas),
                                            new SqlParameter("@cerraduras", cerraduras));

                    if (resultado > 0)
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        return BadRequest("La cobertura no se pudo crear");
                    }

                }
                else
                {
                    return BadRequest("El modelo recibido no es valido");
                }


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateCobertura/{id}")]
        public async Task<IActionResult> UpdateCobertura(int id, Cobertura cobertura)
        {
            try
            {

                if (ModelState.IsValid && cobertura.Nombre != null)
                {
                    var idCobertura = id;
                    var nombre = cobertura.Nombre;
                    var responsabilidadCivil = cobertura.ResponsabilidadCivil ?? false;
                    var destruccionTotalAccidentes = cobertura.DestruccionTotalAccidentes ?? false;
                    var cristalesLaterales = cobertura.CristalesLaterales ?? false;
                    var lunetasParabrisas = cobertura.LunetasParabrisas ?? false;
                    var cerraduras = cobertura.Cerraduras ?? false;

                    var resultado = await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_UPDATECOBERTURAS @id, @nombre, @responsabilidadCivil, @destruccionTotalAccidentes, @cristalesLaterales, @lunetasParabrisas, @cerraduras",
                                            new SqlParameter("@id", idCobertura),
                                            new SqlParameter("@nombre", nombre),
                                            new SqlParameter("@responsabilidadCivil", responsabilidadCivil),
                                            new SqlParameter("@destruccionTotalAccidentes", destruccionTotalAccidentes),
                                            new SqlParameter("@cristalesLaterales", cristalesLaterales),
                                            new SqlParameter("@lunetasParabrisas", lunetasParabrisas),
                                            new SqlParameter("@cerraduras", cerraduras));

                    if (resultado > 0)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("La cobertura no se pudo modificar");
                    }

                }
                else
                {
                    return BadRequest("El modelo recibido no es valido");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("DeleteCobertura/{id}")]
        public async Task<IActionResult> DeleteCobertura(int id)
        {
            try
            {

                var idCobertura = id;

                var resultado = await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_DELETECOBERTURAS @id",
                                         new SqlParameter("@id", idCobertura));

                if (resultado > 0)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest($"No se puedo eliimnar la cobertura con el id: {id}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
