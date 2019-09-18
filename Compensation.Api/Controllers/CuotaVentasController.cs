using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compensaction.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Compensation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuotaVentasController : ControllerBase
    {

        private readonly CompensationDbContext _context;


        public CuotaVentasController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/FlatComision
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCCuotaVentas>>> GetCuotaVentas()
        {



            return await _context.PCCuotaVentas.ToListAsync();


        }

        // GET: api/Cuotaventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCCuotaVentas>> GetCuotaVentas(int id)
        {
            var pCCuotaVentas = await _context.PCCuotaVentas.FindAsync(id);

            if (pCCuotaVentas == null)
            {
                return NotFound();
            }

            return pCCuotaVentas;
        }

        // GET: api/Historico/5
        [HttpGet("GetCuotaVendedor/{id}")]
        public async Task<ActionResult<IEnumerable<PCCuotaVentas>>> GetCuotaVendedor(string id)
        {
            var resumen = await _context.PCCuotaVentas.Where(r => r.Vendedor == id).ToListAsync();

            if (resumen == null)
            {
                return NotFound();
            }

            return resumen;



        }

        // POST: api/Cuotaventas
        [HttpPost]
        public async Task<ActionResult<PCCuotaVentas>> PostCuotaventas(PCCuotaVentas pCCuotaVentas )
        {

            PCTasaAñoMes pCTasaAñoMes = new PCTasaAñoMes();
            pCTasaAñoMes = _context.PCTasaAñoMes.Where(t => t.Año == pCCuotaVentas.Año && t.Mes == pCCuotaVentas.Mes).FirstOrDefault();
            if (pCTasaAñoMes!=null)
            {
                pCCuotaVentas.TasaUsd = pCTasaAñoMes.Tasa;
                pCCuotaVentas.Cuota = pCCuotaVentas.CuotaUsd * pCCuotaVentas.TasaUsd;
            }
            
            
            _context.PCCuotaVentas.Add(pCCuotaVentas);
            
            await _context.SaveChangesAsync();
            _context.Database.ExecuteSqlCommand("PCSpActualizaCuotaVentas @p0, @p1", pCCuotaVentas.Año, pCCuotaVentas.Mes);
            return CreatedAtAction("GetCuotaVentas", new { id = pCCuotaVentas.Id }, pCCuotaVentas);
        }

        // PUT: api/CuotaVentas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuotaVentas(int id, PCCuotaVentas pCCuotaVentas )
        {
            if (id != pCCuotaVentas.Id)
            {
                return BadRequest();
            }

            PCTasaAñoMes pCTasaAñoMes = new PCTasaAñoMes();
            pCTasaAñoMes = _context.PCTasaAñoMes.Where(t => t.Año == pCCuotaVentas.Año && t.Mes == pCCuotaVentas.Mes).FirstOrDefault();
            if (pCTasaAñoMes != null)
            {
                pCCuotaVentas.TasaUsd = pCTasaAñoMes.Tasa;
                pCCuotaVentas.Cuota = pCCuotaVentas.CuotaUsd * pCCuotaVentas.TasaUsd;
            }

           
            _context.Entry(pCCuotaVentas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _context.Database.ExecuteSqlCommand("PCSpActualizaCuotaVentas @p0, @p1", pCCuotaVentas.Año, pCCuotaVentas.Mes);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuotaVentasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/CuotaVentas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PCCuotaVentas>> Delete(int id)
        {
            var pCCuotaVentas = await _context.PCCuotaVentas.FindAsync(id);
            if (pCCuotaVentas == null)
            {
                return NotFound();
            }


            _context.PCCuotaVentas.Remove(pCCuotaVentas);
            await _context.SaveChangesAsync();

            return pCCuotaVentas;
        }



        private bool CuotaVentasExists(int id)
        {
            return _context.PCCuotaVentas.Any(e => e.Id == id);
        }



    }
}