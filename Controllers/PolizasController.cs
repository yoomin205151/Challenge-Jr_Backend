using Microsoft.AspNetCore.Mvc;
using AdminPolizasAPI.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AdminPolizasAPI;
using Microsoft.Data.SqlClient;

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

        [HttpGet("GetAllPolizas")]
        public async Task<IActionResult> GetAllPolizas()
        {
            try
            {

                var poliza = await _dbContext.Polizas.FromSqlRaw("EXEC SP_LISTPOLIZAS")
                    .AsNoTracking()
                    .ToListAsync();

                if (poliza.Count > 1)
                {
                    return Ok(poliza);
                }
                else
                {
                    return NotFound("No existen registros de polizas");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetPolizaById/{id}")]
        public IActionResult GetPolizaById(int id)
        {
            try
            {
                var poliza = _dbContext.Polizas.Find(id);
                if (poliza == null)
                {
                    return NotFound($"No se encontró una poliza con el ID: {id}");
                }
                return Ok(poliza);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("CreatePolizas")]
        public async Task<IActionResult> CreatePolizas(Poliza poliza)
        {
            try
            {

                if (ModelState.IsValid && poliza.Nombre != null)
                {

                    var nombre = poliza.Nombre;

                    var resultado = await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_CREATEPOLIZAS @nombre",
                                            new SqlParameter("@nombre", nombre));

                    if (resultado > 0)
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        return BadRequest("La poliza no se pudo crear");
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

        [HttpPut("UpdatePoliza/{id}")]
        public async Task<IActionResult> UpdatePoliza(int id, Poliza poliza)
        {
            try
            {

                if (ModelState.IsValid && poliza.Nombre != null)
                {
                    var idPoliza = id;
                    var nombre = poliza.Nombre;

                    var resultado = await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_UPDATEPOLIZAS @id, @nombre",
                                            new SqlParameter("@id", idPoliza),
                                            new SqlParameter("@nombre", nombre));

                    if (resultado > 0)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("La poliza no se pudo modificar");
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

        [HttpDelete("DeletePoliza/{id}")]
        public async Task<IActionResult> DeletePoliza(int id)
        {
            try
            {

                var idPoliza = id;

                var resultado = await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_DELETEPOLIZAS @id",
                                         new SqlParameter("@id", idPoliza));

                if (resultado > 0)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest($"No se puedo eliimnar la poliza con el id: {id}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
