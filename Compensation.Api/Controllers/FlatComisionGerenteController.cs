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
    public class FlatComisionGerenteController : ControllerBase
    {

        private readonly CompensationDbContext _context;


        public FlatComisionGerenteController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/FlatComision
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCFlatComisionGerente>>> GetFlatComision()
        {



            return await _context.PCFlatComisionGerente.ToListAsync();


        }

        // GET: api/FlatComision/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCFlatComisionGerente>> GetFlatComision(int id)
        {
            var flatComision = await _context.PCFlatComisionGerente.FindAsync(id);

            if (flatComision == null)
            {
                return NotFound();
            }

            return flatComision;
        }



        // POST: api/FlatComision
        [HttpPost]
        public async Task<ActionResult<PCFlatComisionGerente>> PostFlatComision(PCFlatComisionGerente flatComision)
        {
            _context.PCFlatComisionGerente.Add(flatComision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlatComision", new { id = flatComision.Id }, flatComision);
        }

        // PUT: api/FlatComision/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlatComision(int id, PCFlatComisionGerente flatComision)
        {
            if (id != flatComision.Id)
            {
                return BadRequest();
            }

            _context.Entry(flatComision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlatComisionExists(id))
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
        public async Task<ActionResult<PCFlatComisionGerente>> Delete(int id)
        {
            var flatComision = await _context.PCFlatComisionGerente.FindAsync(id);
            if (flatComision == null)
            {
                return NotFound();
            }


            _context.PCFlatComisionGerente.Remove(flatComision);
            await _context.SaveChangesAsync();

            return flatComision;
        }



        private bool FlatComisionExists(int id)
        {
            return _context.PCFlatComisionGerente.Any(e => e.Id == id);
        }


    }
}