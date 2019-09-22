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
    public class ResumenOficinaTemporalController : ControllerBase
    {


        private readonly CompensationDbContext _context;


        public ResumenOficinaTemporalController(CompensationDbContext context)
        {
            _context = context;
        }


        // GET: api/FlatComision
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PCResumenOficinaTemporal>>> GetPCResumenOficinaTemporal()
        {

            try
            {
                var resumen = await _context.PCResumenOficinaTemporal.ToListAsync();
                return resumen;
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw;
            }
           
           


        }
    }
}