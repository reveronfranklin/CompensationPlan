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
    public class RangoCumplimientoCuotaGeneralController : ControllerBase
    {


        private readonly CompensationDbContext _context;


        public RangoCumplimientoCuotaGeneralController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/RangoCumplimientoCuotaGeneral
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCRangoCumplimientoCuotaGeneral>>> GetRangoCumplimientoCuotaGeneral()
        {



            return await _context.PCRangoCumplimientoCuotaGeneral.ToListAsync();


        }

        // GET: api/RangoCumplimientoCuotaGeneral/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCRangoCumplimientoCuotaGeneral>> GetRangoCumplimientoCuotaGeneral(int id)
        {
            var pCRangoCumplimientoCuotaGeneral = await _context.PCRangoCumplimientoCuotaGeneral.FindAsync(id);

            if (pCRangoCumplimientoCuotaGeneral == null)
            {
                return NotFound();
            }

            return pCRangoCumplimientoCuotaGeneral;
        }



        // POST: api/RangoCumplimientoCuotaGeneral
        [HttpPost]
        public async Task<ActionResult<PCRangoCumplimientoCuotaGeneral>> PostRangoCumplimientoCuotaGeneral(PCRangoCumplimientoCuotaGeneral pCRangoCumplimientoCuotaGeneral )
        {
            _context.PCRangoCumplimientoCuotaGeneral.Add(pCRangoCumplimientoCuotaGeneral);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRangoCumplimientoCuotaGeneral", new { id = pCRangoCumplimientoCuotaGeneral.Id }, pCRangoCumplimientoCuotaGeneral);
        }

        // PUT: api/RangoCumplimientoCuotaGeneral/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRangoCumplimientoCuotaGeneral(int id, PCRangoCumplimientoCuotaGeneral pCRangoCumplimientoCuotaGeneral )
        {
            if (id != pCRangoCumplimientoCuotaGeneral.Id)
            {
                return BadRequest();
            }

            _context.Entry(pCRangoCumplimientoCuotaGeneral).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RangoCumplimientoCuotaGeneralExists(id))
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PCRangoCumplimientoCuotaGeneral>> Delete(int id)
        {
            var pCRangoCumplimientoCuotaGeneral = await _context.PCRangoCumplimientoCuotaGeneral.FindAsync(id);
            if (pCRangoCumplimientoCuotaGeneral == null)
            {
                return NotFound();
            }


            _context.PCRangoCumplimientoCuotaGeneral.Remove(pCRangoCumplimientoCuotaGeneral);
            await _context.SaveChangesAsync();

            return pCRangoCumplimientoCuotaGeneral;
        }



        private bool RangoCumplimientoCuotaGeneralExists(int id)
        {
            return _context.PCRangoCumplimientoCuotaGeneral.Any(e => e.Id == id);
        }


    }
}