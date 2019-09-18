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
    public class PeriodoController : ControllerBase
    {

        private readonly CompensationDbContext _context;


        public PeriodoController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/Periodo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WSMY686>>> GetPeriodo()
        {



            return await _context.WSMY686.ToListAsync();


        }

        // GET: api/Periodo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WSMY686>> GetPeriodo(int id)
        {
            var periodo = await _context.WSMY686.FindAsync(id);

            if (periodo == null)
            {
                return NotFound();
            }

            return periodo;
        }



        // POST: api/Periodo
        [HttpPost]
        public async Task<ActionResult<WSMY686>> PostPeriodo(WSMY686 periodo)
        {
            _context.WSMY686.Add(periodo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeriodo", new { id = periodo.ID }, periodo);
        }

        // PUT: api/Periodo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeriodo(int id, WSMY686 periodo)
        {
            if (id != periodo.ID)
            {
                return BadRequest();
            }

            _context.Entry(periodo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodoExists(id))
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

        // DELETE: api/Periodo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WSMY686>> Delete(int id)
        {
            var periodo = await _context.WSMY686.FindAsync(id);
            if (periodo == null)
            {
                return NotFound();
            }


            _context.WSMY686.Remove(periodo);
            await _context.SaveChangesAsync();

            return periodo;
        }



        private bool PeriodoExists(int id)
        {
            return _context.WSMY686.Any(e => e.ID== id);
        }

    }
}