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
    public class ProductoCuotaController : ControllerBase
    {


        private readonly CompensationDbContext _context;


        public ProductoCuotaController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/ProductoCuota
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCProductoCuota>>> GetProductoCuota()
        {



            return await _context.PCProductoCuota.ToListAsync();


        }

        // GET: api/ProductoCuota/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCProductoCuota>> GetProductoCuota(int id)
        {
            var pCProductoCuota = await _context.PCProductoCuota.FindAsync(id);

            if (pCProductoCuota == null)
            {
                return NotFound();
            }

            return pCProductoCuota;
        }



        // POST: api/ProductoCuota
        [HttpPost]
        public async Task<ActionResult<PCProductoCuota>> PostProductoCuota(PCProductoCuota pCProductoCuota )
        {
            _context.PCProductoCuota.Add(pCProductoCuota);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductoCuota", new { id = pCProductoCuota.Id }, pCProductoCuota);
        }

        // PUT: api/ProductoCuota/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoCuota(int id, PCProductoCuota pCProductoCuota )
        {
            if (id != pCProductoCuota.Id)
            {
                return BadRequest();
            }

            _context.Entry(pCProductoCuota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoCuotaExists(id))
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

        // DELETE: api/ProductoCuota/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PCProductoCuota>> Delete(int id)
        {
            var pCProductoCuota = await _context.PCProductoCuota.FindAsync(id);
            if (pCProductoCuota == null)
            {
                return NotFound();
            }


            _context.PCProductoCuota.Remove(pCProductoCuota);
            await _context.SaveChangesAsync();

            return pCProductoCuota;
        }



        private bool ProductoCuotaExists(int id)
        {
            return _context.PCProductoCuota.Any(e => e.Id == id);
        }


    }
}