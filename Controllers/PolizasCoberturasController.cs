using Microsoft.AspNetCore.Mvc;
using AdminPolizasAPI.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AdminPolizasAPI;
using Microsoft.Data.SqlClient;
using AdminPolizasAPI.DTOs;


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

        [HttpGet("GetAllPolizasCoberturas")]
        public async Task<IActionResult> GetAllPolizasCoberturas()
        {
            try
            {

                var polizasCoberturas = await _dbContext.PolizasCoberturas
                                                .FromSqlRaw("EXEC SP_LISTPOLIZASCOBERTURAS")
                                                .ToListAsync();

                var polizaCoberturaDTOs = polizasCoberturas.Select(pc => new PolizasCoberturas
                {
                    Id = pc.Id,
                    PolizaId = pc.PolizaId,
                    Poliza = _dbContext.Polizas.FirstOrDefault(p => p.Id == pc.PolizaId),
                    CoberturaId = pc.CoberturaId,
                    Cobertura = _dbContext.Coberturas.FirstOrDefault(c => c.Id == pc.CoberturaId),
                    MontoAsegurado = pc.MontoAsegurado
                }).ToList();


                if (polizasCoberturas.Count > 0)
                {
                    return Ok(polizasCoberturas);
                }
                else
                {
                    return NotFound("No existen registros de polizascoberturas");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetPolizasCoberturasById/{id}")]
        public ActionResult<PolizasCoberturas> GetPolizasCoberturasById(int id)
        {
            try
            {
                var polizasCoberturas = _dbContext.PolizasCoberturas.Find(id);

                if (polizasCoberturas == null)
                {
                    return NotFound($"No se encontró una polizascoberturas con el ID: {id}");
                }

                polizasCoberturas.Poliza = _dbContext.Polizas.FirstOrDefault(p => p.Id == polizasCoberturas.PolizaId);

                polizasCoberturas.Cobertura = _dbContext.Coberturas.FirstOrDefault(c => c.Id == polizasCoberturas.CoberturaId);

                return Ok(polizasCoberturas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("CreatePolizasCoberturas")]
        public async Task<IActionResult> CreatePolizasCoberturas(PolizasCoberturas polizascoberturas)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var polizaId = polizascoberturas.PolizaId;
                    var coberturaId = polizascoberturas.CoberturaId;
                    var monto = polizascoberturas.MontoAsegurado;

                    var resultado = await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_CREATEPOLIZASCOBERTURAS @polizaID, @coberturaID, @montoAsegurado",
                                            new SqlParameter("@polizaID", polizaId),
                                            new SqlParameter("@coberturaID", coberturaId),
                                            new SqlParameter("@montoAsegurado", monto));

                    if (resultado > 0)
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        return BadRequest("La polizascobertura no se pudo crear");
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

        [HttpPut("UpdatePolizasCoberturas/{id}")]
        public async Task<IActionResult> UpdatePolizasCoberturas(int id, PolizasCoberturas polizascoberturas)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var idPolizascoberturas = id;
                    var polizaId = polizascoberturas.PolizaId;
                    var coberturaId = polizascoberturas.CoberturaId;
                    var monto = polizascoberturas.MontoAsegurado;

                    var resultado = await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_UPDATEPOLIZASCOBERTURAS @id, @polizaID, @coberturaID, @montoAsegurado",
                                            new SqlParameter("@id", idPolizascoberturas),
                                            new SqlParameter("@polizaID", polizaId),
                                            new SqlParameter("@coberturaID", coberturaId),
                                            new SqlParameter("@montoAsegurado", monto));

                    if (resultado > 0)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("La polizascobertura no se pudo modificar");
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

        [HttpDelete("DeletePolizasCoberturas/{id}")]
        public async Task<IActionResult> DeletePolizasCoberturas(int id)
        {
            try
            {

                var idPolizascobertura = id;

                var resultado = await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_DELETEPOLIZASCOBERTURAS @id",
                                         new SqlParameter("@id", idPolizascobertura));

                if (resultado > 0)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest($"No se puedo eliimnar la polizascobertura con el id: {id}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
