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
    public class SysFileController : ControllerBase
    {
        private readonly CompensationDbContext _context;


        public SysFileController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/SysFile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCSysFile>>> GetSysFile()
        {
            return await _context.PCSysFile.ToListAsync();
        }

        // GET: api/SysFile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCSysFile>> GetSysFile(int id)
        {            
            var sysFile = await _context.PCSysFile.FindAsync(id);

            if (sysFile == null)
            {
                return NotFound();
            }

            return sysFile;
        }

               
        // POST: api/SysFile
        //[HttpPost]
        public async Task<ActionResult<PCSysFile>> PostSysFile(PCSysFile sysFile)
        {
            _context.PCSysFile.Add(sysFile);

            await _context.SaveChangesAsync();

            //TODO aclarar con franklin
            return CreatedAtAction("GetSysFile", new { id = sysFile.Id }, sysFile);            
        }
        

        // PUT: api/SysFile/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSysFile(int id, PCSysFile sysFile)
        {
            if (id != sysFile.Id)
            {
                return BadRequest();
            }

            _context.Entry(sysFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SysFileExists(id))
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

        // DELETE: api/SysFile/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PCSysFile>> Delete(int id)
        {
            var sysFile = await _context.PCSysFile.FindAsync(id);

            if (sysFile == null)
            {
                return NotFound();
            }
            
            _context.PCSysFile.Remove(sysFile);

            await _context.SaveChangesAsync();

            return sysFile;
        }



        private bool SysFileExists(int id)
        {
            return _context.PCSysFile.Any(e => e.Id == id);
        }


    }
}
