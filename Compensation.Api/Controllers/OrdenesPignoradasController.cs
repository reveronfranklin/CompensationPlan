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
    public class OrdenesPignoradasController : ControllerBase
    {
        private readonly CompensationDbContext _context;


        public OrdenesPignoradasController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/OrdenesPignoradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCOrdenesPignoradas>>> GetOrdenesPignoradas()
        {



            return await _context.PCOrdenesPignoradas.ToListAsync();


        }

        // GET: api/OrdenesPignoradas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCOrdenesPignoradas>> GetOrdenesPignoradas(int id)
        {
            var pCOrdenesPignoradas = await _context.PCOrdenesPignoradas.FindAsync(id);

            if (pCOrdenesPignoradas == null)
            {
                return NotFound();
            }

            return pCOrdenesPignoradas;
        }



        // POST: api/OrdenesPignoradas
        [HttpPost]
        public async Task<ActionResult<PCOrdenesPignoradas>> PostOrdenesPignoradas(PCOrdenesPignoradas pCOrdenesPignoradas )
        {
            _context.PCOrdenesPignoradas.Add(pCOrdenesPignoradas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdenesPignoradas", new { id = pCOrdenesPignoradas.Id }, pCOrdenesPignoradas);
        }

        // PUT: api/OrdenesPignoradas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenesPignoradas(int id, PCOrdenesPignoradas pCOrdenesPignoradas )
        {
            if (id != pCOrdenesPignoradas.Id)
            {
                return BadRequest();
            }

            _context.Entry(pCOrdenesPignoradas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenesPignoradasExists(id))
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

        // DELETE: api/OrdenesPignoradas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PCOrdenesPignoradas>> Delete(int id)
        {
            var pCOrdenesPignoradas = await _context.PCOrdenesPignoradas.FindAsync(id);
            if (pCOrdenesPignoradas == null)
            {
                return NotFound();
            }


            _context.PCOrdenesPignoradas.Remove(pCOrdenesPignoradas);
            await _context.SaveChangesAsync();

            return pCOrdenesPignoradas;
        }



        private bool OrdenesPignoradasExists(int id)
        {
            return _context.PCOrdenesPignoradas.Any(e => e.Id == id);
        }


    }
}