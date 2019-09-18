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
    public class TasaAñoMesController : ControllerBase
    {


        private readonly CompensationDbContext _context;


        public TasaAñoMesController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/TasaAñoMes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCTasaAñoMes>>> GetFlatComision()
        {



            return await _context.PCTasaAñoMes.ToListAsync();


        }

        // GET: api/TasaAñoMes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCTasaAñoMes>> GetTasaAñoMes(int id)
        {
            var tasaAñoMes = await _context.PCTasaAñoMes.FindAsync(id);

            if (tasaAñoMes == null)
            {
                return NotFound();
            }

            return tasaAñoMes;
        }



        // POST: api/TasaAñoMes
        [HttpPost]
        public async Task<ActionResult<PCFlatComision>> PostTasaAñoMes(PCTasaAñoMes pCTasaAñoMes )
        {
            _context.PCTasaAñoMes.Add(pCTasaAñoMes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlatComision", new { id = pCTasaAñoMes.Id }, pCTasaAñoMes);
        }

        // PUT: api/TasaAñoMes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasaAñoMes(int id, PCTasaAñoMes pCTasaAñoMes )
        {
            if (id != pCTasaAñoMes.Id)
            {
                return BadRequest();
            }

            _context.Entry(pCTasaAñoMes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasaAñoMesExists(id))
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

        // DELETE: api/TasaAñoMes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PCTasaAñoMes>> Delete(int id)
        {
            var pCTasaAñoMes = await _context.PCTasaAñoMes.FindAsync(id);
            if (pCTasaAñoMes == null)
            {
                return NotFound();
            }


            _context.PCTasaAñoMes.Remove(pCTasaAñoMes);
            await _context.SaveChangesAsync();

            return pCTasaAñoMes;
        }



        private bool TasaAñoMesExists(int id)
        {
            return _context.PCFlatComision.Any(e => e.Id == id);
        }



    }
}