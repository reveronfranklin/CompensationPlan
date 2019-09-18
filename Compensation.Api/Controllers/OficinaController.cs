using Compensaction.Share;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Compensation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OficinaController : ControllerBase
    {

        private readonly CompensationDbContext _context;


        public OficinaController(CompensationDbContext context)
        {
            _context = context;
        }

        // GET: api/Compensation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCOficina>>> GetOficina()
        {
            _context.Database.ExecuteSqlCommand("PCSpOficina @p0", "");

            return await _context.PCOficina.ToListAsync();


        }

    }
}