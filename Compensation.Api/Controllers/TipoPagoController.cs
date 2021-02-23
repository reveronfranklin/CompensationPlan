using Compensaction.Share;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPagoController : ControllerBase
    {
        private readonly CompensationDbContext _context;


        public TipoPagoController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/TipoPago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCTipoPago>>> GetTipoPago()
        {
            return await _context.PCTipoPago.ToListAsync();
        }

        // GET: api/TipoPago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCTipoPago>> GetTipoPago(int id)
        {            
            var TipoPago = await _context.PCTipoPago.FindAsync(id);

            if (TipoPago == null)
            {
                return NotFound();
            }

            return TipoPago;
        }

               
        // POST: api/TipoPago
        //[HttpPost]
        public async Task<ActionResult<PCTipoPago>> PostTipoPago(PCTipoPago TipoPago)
        {
            _context.PCTipoPago.Add(TipoPago);

            await _context.SaveChangesAsync();

            //TODO aclarar con franklin
            return CreatedAtAction("GetTipoPago", new { id = TipoPago.Id }, TipoPago);            
        }
        

        // PUT: api/TipoPago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoPago(int id, PCTipoPago TipoPago)
        {
            if (id != TipoPago.Id)
            {
                return BadRequest();
            }

            _context.Entry(TipoPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoPagoExists(id))
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

        // DELETE: api/TipoPago/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PCTipoPago>> Delete(int id)
        {
            var TipoPago = await _context.PCTipoPago.FindAsync(id);

            if (TipoPago == null)
            {
                return NotFound();
            }
            
            _context.PCTipoPago.Remove(TipoPago);

            await _context.SaveChangesAsync();

            return TipoPago;
        }



        private bool TipoPagoExists(int id)
        {
            return _context.PCTipoPago.Any(e => e.Id == id);
        }


    }
}
