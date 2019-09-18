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
    public class FlatComisionController : ControllerBase
    {
        private readonly CompensationDbContext _context;


        public FlatComisionController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/FlatComision
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCFlatComision>>> GetFlatComision()
        {



            return await _context.PCFlatComision.ToListAsync();


        }

        // GET: api/FlatComision/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCFlatComision>> GetFlatComision(int id)
        {
            var flatComision = await _context.PCFlatComision.FindAsync(id);

            if (flatComision == null)
            {
                return NotFound();
            }

            return flatComision;
        }



        // POST: api/FlatComision
        [HttpPost]
        public async Task<ActionResult<PCFlatComision>> PostFlatComision(PCFlatComision flatComision)
        {
            _context.PCFlatComision.Add(flatComision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlatComision", new { id = flatComision.Id }, flatComision);
        }
        
        // PUT: api/FlatComision/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlatComision(int id, PCFlatComision flatComision)
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
        public async Task<ActionResult<PCFlatComision>> Delete(int id)
        {
            var flatComision = await _context.PCFlatComision.FindAsync(id);
            if (flatComision == null)
            {
                return NotFound();
            }


            _context.PCFlatComision.Remove(flatComision);
            await _context.SaveChangesAsync();

            return flatComision;
        }



        private bool FlatComisionExists(int id)
        {
            return _context.PCFlatComision.Any(e => e.Id == id);
        }


    }
}
