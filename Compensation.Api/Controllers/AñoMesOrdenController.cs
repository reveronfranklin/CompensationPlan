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
    public class AñoMesOrdenController : ControllerBase
    {


        private readonly CompensationDbContext _context;


        public AñoMesOrdenController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/AñoMesOrden
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCAñoMesOrden>>> GetAñoMesOrden()
        {



            return await _context.PCAñoMesOrden.ToListAsync();


        }

        // GET: api/AñoMesOrden/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PCAñoMesOrden>> GetAñoMesOrden(string id)
        {

                _context.Database.ExecuteSqlCommand("PCAñoOrden @p0", id);

                var pCAñoMesOrden = await _context.PCAñoMesOrden.Where(o => o.Orden == id).FirstAsync();
                if (pCAñoMesOrden == null)
                {
                    return NotFound();
                }

                return pCAñoMesOrden;
            
          
           
        }

    }
}