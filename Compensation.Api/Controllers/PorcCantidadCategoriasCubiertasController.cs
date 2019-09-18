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
    public class PorcCantidadCategoriasCubiertasController : ControllerBase
    {

        private readonly CompensationDbContext _context;


        public PorcCantidadCategoriasCubiertasController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/PorcCantidadCategoriasCubiertas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCPorcCantidadCategoriasCubiertas>>> GetPorcCantidadCategoriasCubiertas()
        {



            return await _context.PCPorcCantidadCategoriasCubiertas.ToListAsync();


        }

        // GET: api/PorcCantidadCategoriasCubiertas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCPorcCantidadCategoriasCubiertas>> GetPorcCantidadCategoriasCubiertas(int id)
        {
            var pCPorcCantidadCategoriasCubiertas = await _context.PCPorcCantidadCategoriasCubiertas.FindAsync(id);

            if (pCPorcCantidadCategoriasCubiertas == null)
            {
                return NotFound();
            }

            return pCPorcCantidadCategoriasCubiertas;
        }



        // POST: api/PorcCantidadCategoriasCubiertas
        [HttpPost]
        public async Task<ActionResult<PCPorcCantidadCategoriasCubiertas>> PostPorcCantidadCategoriasCubiertas(PCPorcCantidadCategoriasCubiertas pCPorcCantidadCategoriasCubiertas )
        {
            _context.PCPorcCantidadCategoriasCubiertas.Add(pCPorcCantidadCategoriasCubiertas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPorcCantidadCategoriasCubiertas", new { id = pCPorcCantidadCategoriasCubiertas.Id }, pCPorcCantidadCategoriasCubiertas);
        }

        // PUT: api/PorcCantidadCategoriasCubiertas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPorcCantidadCategoriasCubiertas(int id, PCPorcCantidadCategoriasCubiertas pCPorcCantidadCategoriasCubiertas )
        {
            if (id != pCPorcCantidadCategoriasCubiertas.Id)
            {
                return BadRequest();
            }

            _context.Entry(pCPorcCantidadCategoriasCubiertas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PorcCantidadCategoriasCubiertasExists(id))
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

        // DELETE: api/PorcCantidadCategoriasCubiertas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PCPorcCantidadCategoriasCubiertas>> Delete(int id)
        {
            var pCPorcCantidadCategoriasCubiertas = await _context.PCPorcCantidadCategoriasCubiertas.FindAsync(id);
            if (pCPorcCantidadCategoriasCubiertas == null)
            {
                return NotFound();
            }


            _context.PCPorcCantidadCategoriasCubiertas.Remove(pCPorcCantidadCategoriasCubiertas);
            await _context.SaveChangesAsync();

            return pCPorcCantidadCategoriasCubiertas;
        }



        private bool PorcCantidadCategoriasCubiertasExists(int id)
        {
            return _context.PCPorcCantidadCategoriasCubiertas.Any(e => e.Id == id);
        }



    }
}